using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClickExample : MonoBehaviour
{
    public Button yourButton;
    [SerializeField] bool selected;
    public AddScene scene;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        ColorBlock cb = btn.colors;
        if (selected)
        {
            
            cb.pressedColor = Color.blue;
            cb.normalColor = Color.blue;
            cb.highlightedColor = Color.blue;
            //btn.enabled = false;
        }
        
        else
        btn.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {
      // if(scene.)
       scene.scene2.allowSceneActivation = false;
       scene.scene1.allowSceneActivation = true;

    }
}
