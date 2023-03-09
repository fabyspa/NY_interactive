using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl
{
    public static AsyncOperation scene1, scene2;

    public static void addScene()
    {
        //GameObject.Find("Canvas");
        scene1 = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        scene2 = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        //scene2.allowSceneActivation = false;

    }
    public static void BackgroundScene(GameObject s)
    {

        s.SetActive(false);

    }

    public static void ActivateScene(GameObject s)
    {
        s.SetActive(true);
    }


}

//if (scene2.allowSceneActivation)
//{
//    Debug.Log("case1");

//    s.SetActive(false);
//    SceneManager.UnloadSceneAsync(1, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

//    scene1.allowSceneActivation = true;
//    scene2.allowSceneActivation = false;


//}
//else
//{
//    s.SetActive(false);
//    Debug.Log("case2");
//    SceneManager.UnloadSceneAsync(1, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
//    scene2.allowSceneActivation = true;
//    scene1.allowSceneActivation = false;
//}

