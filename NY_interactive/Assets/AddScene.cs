using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour
{
    public AsyncOperation scene1, scene2;

    // Start is called before the first frame update
    void Start()
    {
        addScene();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addScene()
    {
        scene1 = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        scene2 = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        AsyncOperation master = SceneManager.UnloadSceneAsync(0);

        scene2.allowSceneActivation = false;

        

    }
}
