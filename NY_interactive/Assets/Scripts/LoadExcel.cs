using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirFishLab.ScrollingList.Demo;


public class LoadExcel : MonoBehaviour
{
    //inizializzo un oggetto Riserva
    public Riserva blankRiserva;
    public List<Riserva> riservaDatabase = new List<Riserva>();
    public List<Riserva> riservaDatabaseType = new List<Riserva>();
    public List<string> type = new List<string>();
    [SerializeField] GameObject scrolling;
    public bool loadedItems = false;
    private string actualType;
    [SerializeField] GameObject point;
    public Transform parent;

    public void Start()
    {
        LoadItemData();
        scrolling.GetComponent<VariableStringListBankRiserva>().ChangeContents();
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
        //tempItem.coord = Convert_coordinates.remapLatLng(coord, 180f, 1900f, 360f, 1700f).ToString(); 
        tempItem.name = name;
        tempItem.descr = descr;

        riservaDatabase.Add(tempItem);
        InstantiatePoints(tempItem);
    }

    //una funzione che ho creato io (sam)
    public string[] GetArrayTypeFromList()
    {
        string[] arrayTemp = new string[riservaDatabase.Count];
        for(int i =0; i<arrayTemp.Length; i++)
        {
            arrayTemp[i] = riservaDatabase[i].type;
        }
        return arrayTemp;
    }
    //altra funzione di sam che potrebbe compromettere tutto il codice
    public void InstantiatePoints(Riserva r)
    {
        if (r.coord != "")
        {
            int[] coord = Convert_coordinates.remapLatLng(r.coord);
            Debug.Log(coord[0]+","+ coord[1]);
            Vector3 v = new Vector3(coord[0], coord[1], 0);
            Vector3 vec3 = parent.transform.InverseTransformPoint(v);
            Instantiate(point, vec3, Quaternion.identity,parent);
        }

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

}
