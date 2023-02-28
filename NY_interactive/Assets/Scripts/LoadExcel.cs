using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirFishLab.ScrollingList;
using System.Linq;
using UnityEngine.UI;


public class LoadExcel : MonoBehaviour
{
    //inizializzo un oggetto Riserva
    public Riserva blankRiserva;
    public List<Riserva> riservaDatabase = new List<Riserva>();
    public List<Riserva> riservaDatabaseType = new List<Riserva>();
    public List<Riserva> ordenList = new List<Riserva>();
   // public Image _image;
    public List<string> type = new List<string>();
    [SerializeField] GameObject scrolling;
    public bool loadedItems = false;
    public string actualType;
    public Dictionary<GameObject, float[]> coord2position = new Dictionary<GameObject, float[]>();
    public GameObject _oldGameObjecct;
    public Transform parent;
    public List<GameObject> pointList = new List<GameObject>();

    
    //Ogetto contenente l'item attivo in questo momento
    public Riserva aItem;
    [SerializeField] GameObject point;



    public void Start()
    {
        LoadItemData();
        scrolling.GetComponent<VariableStringListBankRiserva>().ChangeContents();
        SortListByType();
        GameObject.FindGameObjectWithTag("Info").GetComponent<VariableGameObjectListBankRiserva>().ChangeInfoContents("Tutte");
       // Debug.Log("ITEM "+aItem.coord);
       

    }



    public void LoadItemData()
    {

        //clear database
        riservaDatabase.Clear();
        riservaDatabaseType.Clear();
        type.Clear();
        //READ CSV FILE
        List<Dictionary<string, object>> data = CSVReader.Read("RiserveNEW");
        for (var i = 0; i < data.Count; i++)
        {
            //Debug.Log("i: riga " + i);
            string type = data[i]["Type"].ToString();
            string name = data[i]["Name"].ToString();
            string coord = data[i]["Coord"].ToString();
            string descr = data[i]["Descr"].ToString();
            Sprite sprite = UpdateImage((data[i]["Name"]).ToString());
            AddRiserva(type, name, coord, descr,sprite);

        }
        loadedItems = true;
        GetRiservaTypes();
        AddState();
        /* InstantiatePoints(riservaDatabase,tipo);*/
    }

    public Sprite UpdateImage(string _name)
    {
        Debug.Log("Image updated!");
        var tex = Resources.Load<Sprite>("Images/" + _name);
        if (tex == null) return null;
        return tex;
    }
    //se viene modificato il file excel da esterno facciamo in modo che si aggiorni direttamente la build
    public void ReLoadItemData()
    {
        loadedItems = false;
        LoadItemData();
    }
    void AddRiserva(string type, string name, string coord,  string descr, Sprite sprite)
    {
        Riserva tempItem = new Riserva(blankRiserva);

        tempItem.type = type;
        tempItem.coord = coord;
        tempItem.name = name;
        tempItem.descr = descr;
        tempItem.sprite = sprite;
        riservaDatabase.Add(tempItem);
    }

    //Instanzio i punti passandogli la lista 
    public void InstantiatePoints(List<Riserva> r)
    {
        ClearPoints();
        coord2position.Clear();
        AddState();
        foreach (Riserva c in r) {
            GameObject Tpoint = TransformPoint(c.state);
            float[] coord = Convert_coordinates.remapLatLng(c.coord);
            Vector3 worldSpacePosition = new Vector3(coord[1], coord[0], 0);
            Vector3 localSpacePosition = transform.InverseTransformPoint(worldSpacePosition);
            var instanciated = Instantiate(Tpoint, localSpacePosition, Quaternion.identity, parent);
            pointList.Add(instanciated);
            //Debug.Log(instanciated.transform.localPosition);
           // Debug.Log(c.coord);
            if(!coord2position.ContainsKey(instanciated)) coord2position.Add(instanciated,coord);
           // Debug.Log(string.Join(",", coord2position));
        }
       
    }

    public void CoordToPositionMap()
    {

    }

    public void ClearPoints()
    {
        foreach (GameObject c in pointList)
        {
            GameObject.Destroy(c);
        }
    }

    //aggiunge lo stato alla variabile state
    public void AddState()
    {
       // Debug.Log("addstate");

        foreach (Riserva r in riservaDatabase)
        {
            if (riservaDatabaseType.Contains(r))
            {

                if (r.name == aItem.name)
                {
                    r.state = "selected";
                }
                else r.state = "active";
            }

            else
                r.state = "unselected";
        }

        
    }

    public void ChangeStateTo(GameObject g, string newstate)
    {
        Vector3 highlights = new Vector3((float)1.5, (float)1.5, 0);
        Vector3 grande = new Vector3((float)0.8, (float)0.8, 0);


        Riserva r = GetRiservaByCoord(g);
        if (_oldGameObjecct != null)
        {
            Riserva oldR = GetRiservaByCoord(_oldGameObjecct);
            oldR.state = "active";
            _oldGameObjecct.transform.localScale = grande;
        }
        r.state = newstate;
        g.transform.localScale = highlights;
        _oldGameObjecct = g;

    }
    //gestisco scala punti
    public GameObject TransformPoint(string state)
    {
        GameObject t = point;
        Vector3 piccolo = new Vector3((float)0.4, (float)0.4, 0);
        Vector3 grande = new Vector3((float)0.8, (float)0.8, 0);
        Vector3 highlights = new Vector3((float)1.5, (float)1.5, 0);
        switch (state)
        {
            case "active":
                t.transform.localScale = grande;
                break;
            case "selected":
                t.transform.localScale = highlights;
                break;
            default:
                t.transform.localScale = piccolo;
                break;
        }
         
            return t;
    }

    //torna tutti i tipi di riserve diverse
    public void GetRiservaTypes()
    {
        if (loadedItems == false) LoadItemData();
        
        foreach (Riserva r in riservaDatabase)
        {
            if (!type.Contains(r.type)){
                type.Add(r.type);
            }
        }

        //Debug.Log(type);
    }

    public  List<Riserva> LoadRiservaByType(string type)
    {
        if (actualType != type)
        {
            Debug.Log("CARICO LA LISTA PER TIPO");
            actualType = type;
            riservaDatabaseType.Clear();
            if (loadedItems == false) LoadItemData();
            foreach (Riserva r in riservaDatabase)
            {
                if (r.type.ToUpper() == type.ToUpper())
                {
                    riservaDatabaseType.Add(r);
                }
            }
        }

        return riservaDatabaseType;

    }
    public Riserva LoadRiservaByName(string name)
    {

        foreach (Riserva r in riservaDatabase)
        {
            if (r.name == name) return r;
        }

        return null;

    }

    public Riserva GetRiservaByCoord(GameObject p)
    {
        
        foreach(Riserva r in riservaDatabase)
        {
            float[] coord = Convert_coordinates.remapLatLng(r.coord);
            var value = new float[2];
            coord2position.TryGetValue(p, out value);
            //Debug.Log("val "+ string.Join(" ,", value));
            //Debug.Log("coord " + string.Join(" ,", coord));

            if (Enumerable.SequenceEqual(coord,value))
            {
                //Debug.Log("SELEZIONATA "+ r.name);
                return r;
            }
           
        }
        return null;
    }

    public List<Riserva> SortListByType()
    {
        foreach (string t in type)
        {
            ordenList.AddRange(LoadRiservaByType(t));
        }
        return ordenList;

    }

}
