using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour
{
    public Button yourButton;
    public GameObject canvas;
    public bool ris,par;
    //public bool selected;
    //public string scenename;


    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        canvas = transform.root.gameObject;

        par = false;
        ris = true;
        

        //ColorBlock cb = btn.colors;

        //se il bottone ti manda alla stessa scena
        //if (this.gameObject.tag == SceneManager.)
       // {

            //SceneManager.GetActiveScene().name
           
            //cb.pressedColor = Color.blue;
            //cb.normalColor = Color.blue;
            //cb.highlightedColor = Color.blue;
            btn.onClick.AddListener(ClickListener.TaskOnClickDisabled);
            
       // }
        
        //else
        btn.onClick.AddListener(ClickListener.TaskOnClickActive);

    }

    //void TaskOnClickActive()
    //{
    //    Debug.Log("Current scene " + SceneManager.GetActiveScene().name + " nome tag " + this.gameObject.tag);
    //    if (SceneManager.GetActiveScene().name == "PARCHI")
    //    {
    //        canvas.SetActive(par);
    //        par = !par;
    //    }
    //    else
    //    {
    //        canvas.SetActive(ris);
    //        ris = !ris;
    //    }
    //}



    //void TaskOnClickDisabled()
    //{
    //    Debug.Log("BOTTONE DISABILITATO");
    //    canvas.SetActive(false);

    //}

}
