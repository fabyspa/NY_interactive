using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject c1, c2;

    // Start is called before the first frame update
    void Start()
    {
        SceneControl.addScene();
        SceneControl.BackgroundScene(c2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
