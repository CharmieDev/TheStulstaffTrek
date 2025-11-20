
using System;
using Unity.GraphToolkit.Editor;
using UnityEditor;
using UnityEngine;

[Serializable]
[Graph(AssetExtention)]
public class DialogueGraph : Graph
{
    public const string AssetExtention = "dialoguegraph";

    [MenuItem("Assets/Create/Dialogue Graph")]
    private static void CreateAssetFile()
    {
        GraphDatabase.PromptInProjectBrowserToCreateNewAsset<DialogueGraph>();
    }
    
}
