using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AirFishLab.ScrollingList;
using System.Linq;

public class Loader
{
    public enum SceneName
    {
        PARCHI,
        RISERVE
    }
    public static SceneName currentScene;
    private static bool isParkSceneLoaded;
    private static bool isRiservaSceneLoaded;
    private static bool toggle;
    public static void SwitchScene()
    {
        // Salva lo stato degli oggetti della scena corrente
        SaveToggleState();
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
        GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioSource>().Play();

        LoadSceneState();

    }
    public static void EnableDisableSceneObjects()
    {
        if (currentScene == SceneName.PARCHI && SceneManager.GetSceneByName(SceneName.PARCHI.ToString()).isLoaded)
        {
            GameObject[] objectsInScene = SceneManager.GetSceneByName(SceneName.RISERVE.ToString()).GetRootGameObjects();
            // Salva lo stato di ogni oggetto
            foreach (GameObject obj in objectsInScene)
            {
                obj.SetActive(false);
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName.PARCHI.ToString()));
            GameObject[] objectsInSceneActive = SceneManager.GetActiveScene().GetRootGameObjects();
            // Salva lo stato di ogni oggetto
            foreach (GameObject obj in objectsInSceneActive)
            {
                obj.SetActive(true);
            }
            GameObject.FindGameObjectWithTag("TOGGLE").GetComponent<Toggle>().isOn = toggle;
            var loadexcel = GameObject.FindObjectOfType<LoadExcelParchi>();
            loadexcel.info.GetComponent<CircularScrollingListRiserva>()._listPositionCtrl.first = true;

            //if (isRiservaSceneLoaded) SceneManager.UnloadSceneAsync(SceneName.RISERVE.ToString());
            ResetScroll(SceneName.PARCHI);

            isParkSceneLoaded = true;
            isRiservaSceneLoaded = false;
        }
        else if (currentScene == SceneName.RISERVE && SceneManager.GetSceneByName(SceneName.RISERVE.ToString()).isLoaded)
        {
            GameObject[] objectsInScene = SceneManager.GetSceneByName(SceneName.PARCHI.ToString()).GetRootGameObjects();
            // Salva lo stato di ogni oggetto
            foreach (GameObject obj in objectsInScene)
            {
                obj.SetActive(false);
            }
            // Disabilita gli oggetti della scena Park
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(SceneName.RISERVE.ToString()));
            GameObject[] objectsInSceneActive = SceneManager.GetActiveScene().GetRootGameObjects();
            // Salva lo stato di ogni oggetto
            foreach (GameObject obj in objectsInSceneActive)
            {
                obj.SetActive(true);
            }
            var loadexcel = GameObject.FindObjectOfType<LoadExcel>();
            GameObject.FindGameObjectWithTag("TOGGLE").GetComponent<Toggle>().isOn = toggle;
            loadexcel.info.GetComponent<CircularScrollingListRiserva>()._listPositionCtrl.first = true;
            ResetScroll(SceneName.RISERVE);
            loadexcel.SetFocusOnTheTop();
            //if (isParkSceneLoaded) SceneManager.UnloadSceneAsync(SceneName.PARCHI.ToString());
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
        Debug.Log("LoaderScene6");
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
    private static bool SaveToggleState()
    {
        toggle = GameObject.FindGameObjectWithTag("TOGGLE").GetComponent<Toggle>().isOn;
        return toggle;
    }

    private static void ResetScroll(SceneName scene)
    {
        if (scene == SceneName.RISERVE)
        {
            LoadExcel loadExcel = GameObject.FindObjectOfType<LoadExcel>();
            loadExcel.ResetScroll(); 
        }
        if (scene == SceneName.PARCHI)
        {
            LoadExcelParchi loadExcel = GameObject.FindObjectOfType<LoadExcelParchi>();
            loadExcel.ResetScroll();
        }
    }
}
