using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AddScene : MonoBehaviour
{
    private Loader.SceneName currentScene;
    private bool isParkSceneLoaded;
    private bool isRiservaSceneLoaded;

    private void Awake()
    {
        // Precarica entrambe le scene in background
        SceneManager.LoadSceneAsync(Loader.SceneName.PARCHI.ToString(), LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync(Loader.SceneName.RISERVE.ToString(), LoadSceneMode.Additive);

    }
    private void Start()
    {
        // Imposta la scena corrente sulla scena iniziale (Park)
        currentScene = Loader.SceneName.PARCHI;
        Loader.SetCurrentScene(currentScene);
        // Abilita/disabilita i GameObject delle scene in base alla scena corrente
        Loader.EnableDisableSceneObjects();
    }
}
