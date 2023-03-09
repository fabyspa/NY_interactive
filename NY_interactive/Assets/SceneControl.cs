using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl
{
    public static AsyncOperation scene1, scene2;

    public static void addScene()
    {
        scene1 = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        scene2 = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        scene2.allowSceneActivation = false;

    }


}


