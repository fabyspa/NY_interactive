using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl
{
    public static AsyncOperation sceneR, sceneP;
    public static Scene riserve, parchi;

    public static void addScene()
    {
        sceneR = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        sceneP = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        //sceneP.allowSceneActivation = false;
        riserve = SceneManager.GetSceneByBuildIndex(1);
        parchi = SceneManager.GetSceneByBuildIndex(2);
    }

    public static void TaskOnClickActive(Scene sceneToActivate)
    {

    }


    public static void TaskOnClickDisabled(Scene sceneToDisabled)
    {
        Debug.Log("DISABLED"+ sceneToDisabled.name);
        if (sceneToDisabled.name == "RISERVE")
        {
            //sceneR.allowSceneActivation = false;
            SceneManager.SetActiveScene(parchi);
            GameObject.FindGameObjectWithTag("CANVARISERVE").SetActive(false);

        }


    }


}


