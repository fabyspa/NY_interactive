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
    [SerializeField] GameObject thirdFilter;
    private int countTypes;
    [SerializeField] GameObject gameobjectToClone;
    int index = -1;
    Transform nome;
    //private bool stopped = true;
    
    //attenzione, l'ho messo public
    public float scrollPos = 0;
    float[] pos;
    int posisi = 0;


    //scorri avanti di una posizione
    public void next()
    {
        if(posisi<pos.Length - 1)
        {
            posisi += 1;
            scrollPos= pos[posisi];
        }
    }

    //torna indietro di una posizione
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

        database = GameObject.FindAnyObjectByType<LoadExcel>();
        countTypes = database.type.Count;
        
        TypesToText(2);
    }

    //instanziamo i figli in base ai diversi tipi
    public void TypesToText(int numfilter)
    {
        if (gameobjectToClone != null && numfilter == 2)
        {

            if (this.name == "SecondFilter")
            {
                foreach (string t in database.type)
                {
                    var instanciated = Instantiate(gameobjectToClone, this.transform);
                    instanciated.GetComponent<TextMeshProUGUI>().text = t;
                }
            }
            
        }
        if (gameobjectToClone != null && numfilter == 3)
        {
           if(thirdFilter!= null)
            {
                foreach (Transform child in thirdFilter.transform) Destroy(child.gameObject);
                foreach (Riserva r in database.riservaDatabaseType)
                {
                    var instanciated= Instantiate(thirdFilter.GetComponent<SwipeControlBase>().gameObjectToClone, thirdFilter.transform);
                    instanciated.transform.Find("Nome").GetComponent<TextMeshProUGUI>().text= r.name;
                    instanciated.transform.Find("Descr").GetComponent<TextMeshProUGUI>().text = r.descr;


                }
            }
         
        }
    }
    // Update is called once per frame
    void Update()
    {
        //creo un array lungo tanto quanto il numero di oggetti dentro al content
        pos = new float[transform.childCount];
        //distanza scrolling
        float distance = 1f / (pos.Length - 1f);
        for( int i= 0; i< pos.Length; i++)
        {
            pos[i] = distance * i;
        }

       if (Input.GetMouseButton(0))
        {
            scrollPos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            // quando il mouse è premuto se la posizione supera metà della distanza indicata passa alla posizione successiva o precedente
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

            
            database.LoadRiservaByType(database.type[i - 1]);
            TypesToText(3);
            //thirdFilter.GetComponent<SwipeControlBase>().reset = true;
            thirdFilter.GetComponent<SwipeControlBase>().ResetScroll();
           

            //Debug.Log(this.name);
            //TypesToText(gameobjectToClone);

        }

        index = i;
    }
}
