using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClickExample : MonoBehaviour
{
    public Button yourButton;
    [SerializeField] bool selected;
    public GameObject s;


    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        SceneControl.ActivateScene(s);

        //ColorBlock cb = btn.colors;
        if (selected)
        {
            
            //cb.pressedColor = Color.blue;
            //cb.normalColor = Color.blue;
            //cb.highlightedColor = Color.blue;
           
        }
        
        else
        btn.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {

        SceneControl.BackgroundScene(s);

    }
}
