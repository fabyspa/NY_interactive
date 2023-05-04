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
        //yield return new WaitForSeconds(0.1f);
        var asyncLoadLevel1 = SceneManager.LoadSceneAsync(Loader.SceneName.RISERVE.ToString(), LoadSceneMode.Additive);
        while (!asyncLoadLevel1.isDone)
        {
            yield return null;
        }

        currentScene = Loader.SceneName.RISERVE;
        Loader.SetCurrentScene(currentScene);

        var asyncLoadLevel = SceneManager.LoadSceneAsync(Loader.SceneName.PARCHI.ToString(), LoadSceneMode.Additive);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }


        //GameObject[] objectsInScene = SceneManager.GetSceneByName(Loader.SceneName.PARCHI.ToString()).GetRootGameObjects();
        //// Salva lo stato di ogni oggetto
        //foreach (GameObject obj in objectsInScene)
        //{
        //    obj.SetActive(false);
        //}
        // Abilita/disabilita i GameObject delle scene in base alla scena corrente
        Loader.EnableDisableSceneObjects();
    
    }
}
