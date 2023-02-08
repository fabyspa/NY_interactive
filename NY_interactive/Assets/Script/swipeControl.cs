using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipeControl : MonoBehaviour
{
    [SerializeField]
    GameObject scrollbar;

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
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp (scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.01f); //0.01 f corrisponde all'easing


                }

            }
        }


    }
}
