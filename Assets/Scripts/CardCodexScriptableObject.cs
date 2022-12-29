using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CardCodexScriptableObject : ScriptableObject
{
    public List<CardScriptableObject> cards;

    #if UNITY_EDITOR
    [MenuItem("Assets/Create/CardCodex")]
    public static void CreateCardCodex()
    {
        string path = EditorUtility.SaveFilePanelInProject(
            "Save Codex", "New Codex", "Asset", "Save Codex", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(CreateInstance<CardCodexScriptableObject>(), path);
    }
    #endif   
}
    

