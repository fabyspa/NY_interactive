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
        StartCoroutine(SceneLoader());
        // Precarica entrambe le scene in background
        

    }
    private void Start()
    {
        // Imposta la scena corrente sulla scena iniziale (Park)
       
    }

    private IEnumerator SceneLoader()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadSceneAsync(Loader.SceneName.RISERVE.ToString(), LoadSceneMode.Single);
        SceneManager.LoadSceneAsync(Loader.SceneName.PARCHI.ToString(), LoadSceneMode.Additive);

        //SceneManager.LoadSceneAsync(Loader.SceneName.RISERVE.ToString(), LoadSceneMode.Additive);
        currentScene = Loader.SceneName.RISERVE;
        Loader.SetCurrentScene(currentScene);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.1f);
        // Abilita/disabilita i GameObject delle scene in base alla scena corrente
        Loader.EnableDisableSceneObjects();
    }
}
