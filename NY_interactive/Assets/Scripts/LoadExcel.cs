using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirFishLab.ScrollingList;


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
    }



    public void LoadItemData()
    {

        //clear database
        riservaDatabase.Clear();
        riservaDatabaseType.Clear();
        type.Clear();
        //READ CSV FILE
        List<Dictionary<string, object>> data = CSVReader.Read("Riserve");
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

        foreach (Riserva c in r) {
            GameObject Tpoint = TransformPoint(c.state);
            float[] coord = Convert_coordinates.remapLatLng(c.coord);
            Vector3 worldSpacePosition = new Vector3(coord[1], coord[0], 0);
            Vector3 localSpacePosition = transform.InverseTransformPoint(worldSpacePosition);
            pointList.Add(Instantiate(Tpoint, localSpacePosition, Quaternion.identity,parent));

           // Debug.Log(c.coord);
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
        int i = 0;
        foreach(Riserva t in riservaDatabase)
        {
            i++;
            t.state = assignItem(t);
            Debug.Log(t.name);
        }
        
    }

    //assegna lo stato all'oggetto
    public string assignItem(Riserva t)
    {
        string state = "unselected";

            foreach(Riserva r in riservaDatabaseType)
            {
                if (t.name == r.name || t.type=="Tutte" )
                {
                    if (t.name == aItem.name)
                    {
                        state = "active";
                    }
                   
                        state = "selected";
                }
            }

        return state;
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

    public List<Riserva> SortListByType()
    {
        foreach (string t in type)
        {
            ordenList.AddRange(LoadRiservaByType(t));
        }
        return ordenList;

    }

}
