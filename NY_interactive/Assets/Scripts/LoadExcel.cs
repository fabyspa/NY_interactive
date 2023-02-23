using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirFishLab.ScrollingList;
using System.Linq;


public class LoadExcel : MonoBehaviour
{
    //inizializzo un oggetto Riserva
    public Riserva blankRiserva;
    public List<Riserva> riservaDatabase = new List<Riserva>();
    public List<Riserva> riservaDatabaseType = new List<Riserva>();
    public List<Riserva> ordenList = new List<Riserva>();

    public List<string> type = new List<string>();
    [SerializeField] GameObject scrolling;
    public bool loadedItems = false;
    private string actualType;
    public Dictionary<Vector3, float[]> coord2position = new Dictionary<Vector3, float[]>();

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

            AddRiserva(type, name, coord, descr);

        }
        loadedItems = true;
        GetRiservaTypes();
        AddState();
        /* InstantiatePoints(riservaDatabase,tipo);*/
    }


    //se viene modificato il file excel da esterno facciamo in modo che si aggiorni direttamente la build
    public void ReLoadItemData()
    {
        loadedItems = false;
        LoadItemData();
    }
    void AddRiserva(string type, string name, string coord,  string descr)
    {
        Riserva tempItem = new Riserva(blankRiserva);

        tempItem.type = type;
        tempItem.coord = coord;
        tempItem.name = name;
        tempItem.descr = descr;
       
        riservaDatabase.Add(tempItem);
    }

    //Instanzio i punti passandogli la lista 
    public void InstantiatePoints(List<Riserva> r)
    {
        ClearPoints();
        coord2position.Clear();

        foreach (Riserva c in r) {
            GameObject Tpoint = TransformPoint(c.state);
            float[] coord = Convert_coordinates.remapLatLng(c.coord);
            Vector3 worldSpacePosition = new Vector3(coord[1], coord[0], 0);
            Vector3 localSpacePosition = transform.InverseTransformPoint(worldSpacePosition);
            var instanciated = Instantiate(Tpoint, localSpacePosition, Quaternion.identity, parent);
            pointList.Add(instanciated);
            //Debug.Log(instanciated.transform.localPosition);
           // Debug.Log(c.coord);
            if(!coord2position.ContainsKey(instanciated.transform.localPosition)) coord2position.Add(instanciated.transform.localPosition,coord);
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
        foreach(Riserva r in riservaDatabase)
        {
            if (riservaDatabaseType.Contains(r))
                r.state = "active";
            else
                r.state = "unselected";
        }

        
    }


    //gestisco scala punti
    public GameObject TransformPoint(string state)
    {
        GameObject t = point;
        Vector3 piccolo = new Vector3((float)0.6, (float)0.6, 0);
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

    public Riserva GetRiservaByCoord(Vector3 position)
    {
        
        foreach(Riserva r in riservaDatabase)
        {
            float[] coord = Convert_coordinates.remapLatLng(r.coord);
            var value = new float[2];
            coord2position.TryGetValue(position, out value);
            //Debug.Log("val "+ string.Join(" ,", value));
            //Debug.Log("coord " + string.Join(" ,", coord));

            if (Enumerable.SequenceEqual(coord,value))
            {
                Debug.Log(r.name);
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
