using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirFishLab.ScrollingList;
using System.Linq;
using UnityEngine.UI;


public class LoadExcelParchi : MonoBehaviour
{
    //inizializzo un oggetto Riserva
    public Parco blankParco;
    public List<Parco> parchiDatabase = new List<Parco>();
    [SerializeField] public GameObject info;
    public bool loadedItems = false;
    
    public Dictionary<GameObject, float[]> coord2position = new Dictionary<GameObject, float[]>();
    public GameObject _oldGameObjecct;
    public Transform parent;
    public List<GameObject> pointList = new List<GameObject>();
    //Ogetto contenente l'item attivo in questo momento
    public Parco aItem;
    [SerializeField] GameObject point;
    public GameObject selectedPoint;



    public void Start()
    {
        LoadItemData();
        info.GetComponent<VariableGameObjectListBankParco>().ChangeInfoContents();
        // Debug.Log("ITEM "+aItem.coord);
    }


    public void ResetScroll()
    {
        var i = FindObjectOfType<CircularScrollingListRiserva>();
        i._isInitialized = false;
        i.Initialize();
        info.GetComponent<VariableGameObjectListBankParco>().ChangeInfoContents();

    }
    public void LoadItemData()
    {

        //clear database
        parchiDatabase.Clear();
        //READ CSV FILE
        List<Dictionary<string, object>> data = CSVReader.Read("Parchi_DEF");
        for (var i = 0; i < data.Count; i++)
        {
            string name = data[i]["Name_ITA"].ToString();
            string coord = data[i]["Coord"].ToString();
            string descr = data[i]["Descr_ITA"].ToString();
            string anno = data[i]["Anno"].ToString();
            string sup = data[i]["Sup"].ToString();
            string region = data[i]["Regione"].ToString();
            string name_eng = data[i]["Name_ENG"].ToString();
            string descr_eng = data[i]["Descr_ENG"].ToString();
            if (name_eng == "")
            {
                name_eng = name;
            }
            //string luogo = data[i]["Luogo"].ToString();
            
            //Sprite sprite = UpdateImage((data[i]["Name_ITA"]).ToString());
           //Sprite sprite = null;
            AddParco(name, coord, descr,region,sup,anno,name_eng,descr_eng);

        }
        loadedItems = true;
       // AddState();
        /* InstantiatePoints(riservaDatabase,tipo);*/
    }
    //se viene modificato il file excel da esterno facciamo in modo che si aggiorni direttamente la build
    public void ReLoadItemData()
    {
        loadedItems = false;
        LoadItemData();
    }

    void AddParco(string name, string coord,  string descr, string region, string sup, string anno, string name_eng, string descr_eng)
    {
        Parco tempItem = new Parco(blankParco);
        tempItem.coord = coord;
        tempItem.name = name;
        tempItem.descr = descr;
        //tempItem.sprite = sprite;
        tempItem.region = region;
        tempItem.sup = sup;
        tempItem.anno = anno;
        tempItem.name_eng = name_eng;
        tempItem.descr_eng = descr_eng;
        parchiDatabase.Add(tempItem);
    }

    //Instanzio i punti passandogli la lista 
    public void InstantiatePoints(List<Parco> r)
    {
        ClearPoints();
        coord2position.Clear();
        AddState();
        foreach (Parco c in r) {
            float[] coord = Convert_coordinates.remapLatLng(c.coord);
            Vector3 worldSpacePosition = new Vector3(coord[1], coord[0], 0);
            Vector3 localSpacePosition = transform.InverseTransformPoint(worldSpacePosition);
            GameObject Tpoint = TransformPoint(c.state);
            Tpoint.gameObject.GetComponent<Image>().color = ColorPoint(c.state);
            var instanciated = Instantiate(Tpoint, localSpacePosition, Quaternion.identity, parent);
            pointList.Add(instanciated);
            if(!coord2position.ContainsKey(instanciated)) coord2position.Add(instanciated,coord);
        }
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

        foreach (Parco r in parchiDatabase)
        {

            if (r.name == aItem.name)
            {
                r.state = "selected";
            }
            else r.state = "active";
        }


    }

    public void ChangeStateTo(GameObject g, string newstate)
    {
        Parco r = GetParcoByCoord(g);
        if (_oldGameObjecct != null)
        {
            Parco oldR = GetParcoByCoord(_oldGameObjecct);
            oldR.state = "active";
            _oldGameObjecct.transform.localScale = TransformPoint(oldR.state).transform.localScale;
            _oldGameObjecct.gameObject.GetComponent<Image>().color = ColorPoint(oldR.state);
        }
        r.state = newstate;
        if (newstate == "selected")
        {
            Debug.Log("SETASLASTSIBLING");
            selectedPoint = g;
        }
        g.transform.localScale = TransformPoint(r.state).transform.localScale;
        g.gameObject.GetComponent<Image>().color = ColorPoint(r.state);
        _oldGameObjecct = g;
        selectedPoint.transform.SetAsLastSibling();
    }
    //gestisco scala punti
    public GameObject TransformPoint(string state)
    {
        GameObject t = point;
        Vector3 grande = new Vector3((float)0.4, (float)0.4, 0);
        Vector3 highlights = new Vector3((float)0.7, (float)0.7, 0);
        switch (state)
        {
            case "active":
                t.transform.localScale = grande;
                t.gameObject.transform.GetChild(0).gameObject.active = true;
                break;
            case "selected":
                t.transform.localScale = highlights;
                t.gameObject.transform.GetChild(0).gameObject.active = true;
                break;
            default:
                t.gameObject.transform.GetChild(0).gameObject.active = false;
                break;
        }
           
            return t;
    }

    public Color ColorPoint(string state)
    {
        Color c = Color.red;
        if (state== "active")
        {
            ColorUtility.TryParseHtmlString("#A8B1B5", out c);
            c.a = 0.8f;
        }
        else if(state == "selected")
        {
            ColorUtility.TryParseHtmlString("#BEC9CC", out c);
            c.a = 1f;
        }
        return c;
    }


    //torna tutti i tipi di riserve diverse

    public Parco LoadParcoByName(string name)
    {

        foreach (Parco p in parchiDatabase)
        {
            if (p.name == name) return p;
        }

        return null;

    }

    public Parco GetParcoByCoord(GameObject p)
    {
        
        foreach(Parco r in parchiDatabase)
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

}
