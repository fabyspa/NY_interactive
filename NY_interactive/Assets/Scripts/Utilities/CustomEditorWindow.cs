using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomEditorWindow : EditorWindow
{
    [MenuItem("Window/CustomWindow")]

    public static void ShowWindow()
    {
        GetWindow<CustomEditorWindow>("CustomEditorWindow");
    }
    // Start is called before the first frame update
    void OnGUI()
    {
        GUILayout.Label("Reload Item Database", EditorStyles.boldLabel);
        GUILayout.Label("Filtro Foresta Demaniale", EditorStyles.boldLabel);
        if (GUILayout.Button("Reload Items"))
        {
            GameObject.Find("Databases").GetComponent<LoadExcel>().LoadRiservaByType("Foresta Demaniale");
        }if (GUILayout.Button("Foresta Demaniale"))
        {
            GameObject.Find("Databases").GetComponent<LoadExcel>().LoadRiservaByType("Foresta Demaniale");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
