using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class swipeControl : MonoBehaviour
{
    [SerializeField]
    GameObject scrollbar;
    LoadExcel database;
    private int countTypes;
    [SerializeField] GameObject gameobjectToClone;
    int index = 0;
    //private bool stopped = true;

    float scrollPos = 0;
    float[] pos;
    int posisi = 0;


  
    public void next()
    {
        if(posisi<pos.Length - 1)
        {
            posisi += 1;
            scrollPos= pos[posisi];
        }
    }

    public void prev()
    {
        if (posisi>0)
        {
            posisi -= 1;
            scrollPos = pos[posisi];
        }
    }





    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
        database = GameObject.FindAnyObjectByType<LoadExcel>();
        countTypes = database.type.Count;
        Debug.Log(countTypes);
        TypesToText(gameobjectToClone);
    }

    //instanziamo i figli in base ai diversi tipi
    public void TypesToText(GameObject gameObjectToClone)
    {
        if (gameobjectToClone != null)
        {

            if (this.name == "SecondFilter")
            {
                foreach (string t in database.type)
                {
                    var instanciated = Instantiate(gameobjectToClone, this.transform);
                    instanciated.GetComponent<TextMeshProUGUI>().text = t;
                }
            }
            //if (this.name == "ThirdFilter")
            //{
            //    Debug.Log("terzo filtro");
            //    
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        //creo un array lungo tanto quanto il numero di oggetti dentro al content
        pos = new float[transform.childCount];
        //easing
        float distance = 1f / (pos.Length - 1f);
        for( int i= 0; i< pos.Length; i++)
        {
            pos[i] = distance * i;
            //Debug.Log()
        }

       if (Input.GetMouseButton(0))
        {
            scrollPos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for(int i =0; i< pos.Length;i++)
            {
                if (scrollPos < pos[i] + (distance/2) && scrollPos > pos[i] - (distance / 2)){
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp (scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.01f);//0.01 f corrisponde all'easing
                    var index_temp = i;
                    if (index != index_temp)
                    {
                        ChangeIndex(index_temp);
                    }



                }

            }
        }


    }
    void ChangeIndex(int i)
    {
        if (i == 0)
        {
            Debug.Log("TUTTE");
        }
        else
        {
            //Debug.Log(database.type[i - 1]);
            database.LoadRiservaByType(database.type[i - 1]);
            //Debug.Log(this.name);
            //TypesToText(gameobjectToClone);

        }
       
        
    }
}
