using AirFishLab.ScrollingList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAndDelete(GameObject go)
    {
        if(!GameObject.FindFirstObjectByType<CircularScrollingListRiserva>()._listPositionCtrl.isRunning) StartCoroutine(GameObject.FindFirstObjectByType<CircularScrollingListRiserva>()._listPositionCtrl.PlayAndDelete(go));
    }

}
