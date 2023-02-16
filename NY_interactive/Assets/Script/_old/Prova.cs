using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prova : MonoBehaviour
{
    LoadExcel database;
    private int countTypes;
    public int numfilter;
    // Start is called before the first frame update
    void Start()
    {
        database = GameObject.FindAnyObjectByType<LoadExcel>();
        string[] array = new string[0]; 

        if (numfilter == 2)
        {
            countTypes = database.type.Count;
            array = new string[countTypes];
            for (int i = 0; i < database.type.Count; i++)
            {

                array[i] = database.type[i];
                Debug.Log(database.type[i]);
            }

        }
        else if (numfilter == 3)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
