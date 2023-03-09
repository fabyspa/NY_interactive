using UnityEngine;
using UnityEngine.SceneManagement;
public class Loader
{
    public enum SceneName
    {
        PARCHI,
        RISERVE
    }
    private static SceneName currentScene;
    private static bool isParkSceneLoaded;
    private static bool isRiservaSceneLoaded;

    public static void SwitchScene()
    {
        // Salva lo stato degli oggetti della scena corrente
        SaveSceneState();
        // Carica la nuova scena
        if (currentScene == SceneName.PARCHI)
        {
            currentScene = SceneName.RISERVE;
            // Abilita/disabilita i GameObject delle scene in base alla scena corrente
            EnableDisableSceneObjects();
        }
        else
        {
            currentScene = SceneName.PARCHI;
            // Abilita/disabilita i GameObject delle scene in base alla scena corrente
            EnableDisableSceneObjects();
        }
        // Carica lo stato degli oggetti della nuova scena
        LoadSceneState();
    }
    public static void EnableDisableSceneObjects()
    {
        if (currentScene == SceneName.PARCHI)
        {
            // Disabilita gli oggetti della scena Riserva
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName.PARCHI.ToString()));
            if (isRiservaSceneLoaded) SceneManager.UnloadSceneAsync(SceneName.RISERVE.ToString());
            isParkSceneLoaded = true;
            isRiservaSceneLoaded = false;
        }
        else
        {
            // Disabilita gli oggetti della scena Park
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName.RISERVE.ToString()));
            if (isParkSceneLoaded) SceneManager.UnloadSceneAsync(SceneName.PARCHI.ToString());
            isRiservaSceneLoaded = true;
            isParkSceneLoaded = false;
        }
    }
    public static void SaveSceneState()
    {
        // Ottiene tutti gli oggetti della scena corrente
        GameObject[] objectsInScene = SceneManager.GetActiveScene().GetRootGameObjects();
        // Salva lo stato di ogni oggetto
        foreach (GameObject obj in objectsInScene)
        {
            //ObjectState objectState = obj.GetComponent<ObjectState>();
            //if (objectState != null)
            //{
            //    objectState.SaveState();
            //}
        }
    }


    public static void SetCurrentScene(SceneName s)
    {
        currentScene = s;
    }

    public static void LoadSceneState()
    {
        // Ottiene tutti gli oggetti della nuova scena
        GameObject[] objectsInScene = SceneManager.GetActiveScene().GetRootGameObjects();
        // Carica lo stato di ogni oggetto
        foreach (GameObject obj in objectsInScene)
        {
            //ObjectState objectState = obj.GetComponent<ObjectState>();
            //if (objectState != null)
            //{
            //    objectState.LoadState();
            //}
        }
    }
}
