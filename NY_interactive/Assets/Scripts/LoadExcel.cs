using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadExcel : MonoBehaviour
{
    //inizializzo un oggetto Riserva
    public Riserva blankRiserva;
    public List<Riserva> riservaDatabase = new List<Riserva>();
    private bool loadedItems = false;
    public void LoadItemData()
    {

        //clear database
        riservaDatabase.Clear();

        //READ CSV FILE
        List<Dictionary<string, object>> data = CSVReader.Read("RiserveDatabase");
        for (var i = 0; i < data.Count; i++)
        {
            Debug.Log("i: riga " + i);
            string type = data[i]["\"Type\""].ToString();
            string name = data[i]["\"Name\""].ToString();
            string coord = data[i]["\"Coord\""].ToString();
            string descr = data[i]["\"Descr\""].ToString();

            AddRiserva(type, name, coord, descr);

        }
        loadedItems = true;
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

    public void LoadRiservaByType(string type)
    {

        if (loadedItems==false) LoadItemData();
        foreach (Riserva r in riservaDatabase)
        {
            if (r.type== type)
            {
                Debug.Log(r.name);
            }
        }
    }
   
}
