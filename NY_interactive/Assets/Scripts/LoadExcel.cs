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
    [SerializeField] public GameObject scrolling;
    [SerializeField] public GameObject info;
    [SerializeField] VariableGameObjectListBankRiserva VariableGameObjectListBankRiserva;
    public bool loadedItems = false;
    public string actualType;
    public Color actualCol;
    public Dictionary<GameObject, float[]> coord2position = new Dictionary<GameObject, float[]>();
    public GameObject _oldGameObjecct;
    public Transform parent;
    public List<GameObject> pointList = new List<GameObject>();
    public Dictionary<string, string> ita2engType = new Dictionary<string, string>();
    //Ogetto contenente l'item attivo in questo momento
    public Riserva aItem;
    [SerializeField] GameObject point;
    public GameObject selectedPoint;


    public void Start()
    {
        LoadItemData();
        scrolling.GetComponent<VariableStringListBankRiserva>().ChangeContents();
        SortListByType();
        info.GetComponent<VariableGameObjectListBankRiserva>().ChangeInfoContents("Tutte");
        // Debug.Log("ITEM "+aItem.coord);
    }


    public void ResetScroll()
    {
        foreach (var i in GameObject.FindObjectsOfType<CircularScrollingListRiserva>())
        {
           if (i.gameObject.tag== "Type")
            {
                i._isInitialized = false;
                i.Initialize();
                info.GetComponent<VariableGameObjectListBankRiserva>().ChangeInfoContents("Tutte");
            }
        }
        
    }

    public void SetFocusOnTheTop()
    {
        var myKey = coord2position.FirstOrDefault(x => Enumerable.SequenceEqual(x.Value, Convert_coordinates.remapLatLng(aItem.coord))).Key;
        
        myKey.transform.SetAsLastSibling();
    }
    public void LoadItemData()
    {

        //clear database
        riservaDatabase.Clear();
        riservaDatabaseType.Clear();
        type.Clear();
        //READ CSV FILE
        List<Dictionary<string, object>> data = CSVReader.Read("RISERVE01");
        for (var i = 0; i < data.Count; i++)
        {
            string[] type = data[i]["Type"].ToString().Split(",");
            string name = data[i]["Name_ITA"].ToString();
            string coord = data[i]["Coord"].ToString();
            string descr = data[i]["Descr_ITA"].ToString();
            string descr_eng = data[i]["Descr_ENG"].ToString();
            string luogo = data[i]["Luogo"].ToString();
            string anno = data[i]["Anno"].ToString();
            string sup = data[i]["Sup"].ToString();
            string region = data[i]["Regione"].ToString();
            string[] type_eng = data[i]["Type_ENG"].ToString().Split(",");
            string repC = data[i]["RepC"].ToString();
            AddRiserva(type, name, coord, descr,region,sup,anno,luogo,descr_eng,type_eng,repC);

        }
        loadedItems = true;
        GetRiservaTypes();
       // AddState();
        /* InstantiatePoints(riservaDatabase,tipo);*/
    }

    //public Sprite UpdateImage(string _name)
    //{
    //    if (Resources.Load<Sprite>("Images/" + _name) != null)
    //    {
    //        tex = Resources.Load<Sprite>("Images/" + _name);
    //        return tex;
    //    }
    //    return null;
    //}
    //se viene modificato il file excel da esterno facciamo in modo che si aggiorni direttamente la build
    public void ReLoadItemData()
    {
        loadedItems = false;
        LoadItemData();
    }

    void AddRiserva(string[] type, string name, string coord,  string descr, string region, string sup, string anno, string luogo, string descr_eng, string[] type_eng,string repC)
    {
        Riserva tempItem = new Riserva(blankRiserva);

        tempItem.type = type;
        tempItem.coord = coord;
        tempItem.name = name;
        tempItem.descr = descr;
        tempItem.region = region;
        tempItem.sup = sup;
        tempItem.anno = anno;
        tempItem.luogo = luogo;
        tempItem.descr_eng = descr_eng;
        tempItem.type_eng = type_eng;
        tempItem.repC = repC;
        riservaDatabase.Add(tempItem);
    }

    //Instanzio i punti passandogli la lista 
    public void InstantiatePoints(List<Riserva> r)
    {
        ClearPoints();
        coord2position.Clear();
        AddState();
        foreach (Riserva c in r) {
            float[] coord = Convert_coordinates.remapLatLng(c.coord);
            Vector3 worldSpacePosition = new Vector3(coord[1], coord[0], 0);
            Vector3 localSpacePosition = transform.InverseTransformPoint(worldSpacePosition);
            GameObject Tpoint = TransformPoint(c.state);
            Tpoint.gameObject.GetComponent<Image>().color = ColorPoint(c.type, c.state, actualType);
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

    //public void BringUp()
    //{
    //    for(int i=0;i<parent.childCount; i++)
    //    {
    //        if(parent.GetChild(i)== selectedPoint)
    //        {
    //            parent.GetChild(i).transform.SetAsLastSibling();
    //            return;
    //        }
    //    }
        
    //}
    public void ChangeStateTo(GameObject g, string newstate)
    {
        Riserva r = GetRiservaByCoord(g);
        if (_oldGameObjecct != null)
        {
            Riserva oldR = GetRiservaByCoord(_oldGameObjecct);
            oldR.state = "active";
            _oldGameObjecct.transform.localScale = TransformPoint(oldR.state).transform.localScale;
            _oldGameObjecct.gameObject.GetComponent<Image>().color = ColorPoint(oldR.type, oldR.state, actualType);
        }
        r.state = newstate;

        //mi sposta il punto in alto
        if (newstate == "selected")
        {
            selectedPoint = g;
        }
        g.transform.localScale = TransformPoint(r.state).transform.localScale;
        g.gameObject.GetComponent<Image>().color = ColorPoint(r.type, r.state, actualType);
        _oldGameObjecct = g;
        selectedPoint.transform.SetAsLastSibling();
    }
    //gestisco scala punti
    public GameObject TransformPoint(string state)
    {
        GameObject t = point;
        Vector3 piccolo = new Vector3((float)0.15, (float)0.15, 0);
        Vector3 grande = new Vector3((float)0.5, (float)0.5, 0);
        Vector3 highlights = new Vector3((float)0.8, (float)0.8, 0);
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
                t.transform.localScale = piccolo;
                t.gameObject.transform.GetChild(0).gameObject.active = false;
                break;
        }

            return t;
    }

    public Color ColorPoint(string[] type, string state, string filter)
    {
        Color c = Color.red;
        bool checkfilter = true;
        if (filter == "Tutte")
        {
            c = ChangeColor(type[0]);
        }
        else
        {
            foreach (string s in type)
            {
                if( s == filter)
                {
                    c=ChangeColor(s);
                    checkfilter = false;
                }
                if (checkfilter)
                {
                   c= ChangeColor(s);
                }
                
            }

        }
        c.a = 0.8f;
        if (state== "selected")
        {
            ColorUtility.TryParseHtmlString("#AABBB0", out c);
            c.a = 1f;
        }
       
        return c;
    }
    public Color ChangeColor(string type)
    {
        Color c;
        switch (type)
        {
            case "Orientata":
                ColorUtility.TryParseHtmlString("#446658", out c);
                break;
            case "Integrale":
                ColorUtility.TryParseHtmlString("#486C64", out c);
                break;
            case "Popolamento Animale":
                ColorUtility.TryParseHtmlString("#5A705F", out c);
                break;
            case "Biogenetica":
                ColorUtility.TryParseHtmlString("#325C5A", out c);
                break;
            case "Foresta Demaniale o altra area gestita":
                ColorUtility.TryParseHtmlString("#2A4754", out c);
                break;
            default:
                c=Color.red;
                break;
        }
        return c;
    }

    //torna tutti i tipi di riserve diverse
    public void GetRiservaTypes()
    {
        if (loadedItems == false) LoadItemData();
        
        foreach (Riserva r in riservaDatabase)
        {
            foreach(string v in r.type)
            {
                if(!type.Contains(v))
                { 
                    type.Add(v);
                    if (r.type_eng.Count() > 1)
                    {
                        string trad = (string)r.type_eng.GetValue(type.IndexOf(v));
                        if (trad != "" && !ita2engType.ContainsKey(trad))
                        { ita2engType.Add(trad, v); }
                    }
                    else
                    if (r.type_eng[0]!="")
                        { ita2engType.Add(r.type_eng[0], v); }

                }
            }
          
        }

    }

    public  List<Riserva> LoadRiservaByType(string type)
    {
        if(actualType== "Tutte")
        {
            actualCol= ChangeColor(type);
        }
        if (actualType != type)
        {
            riservaDatabaseType.Clear();
            if (loadedItems == false) LoadItemData();
            foreach (Riserva r in riservaDatabase)
            {
                if (r.type.Contains(type))
                {
                   
                    riservaDatabaseType.Add(r);

                }
            }
            actualType = type;
            actualCol = ChangeColor(type);
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
            
            if (Enumerable.SequenceEqual(coord,value))
            {
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
       
        return ordenList.GroupBy(r=>r.name).Select(g=>g.First()).ToList();

    }

}
