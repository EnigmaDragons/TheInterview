// Cristian Pop - https://boxophobic.com/

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HeightFogCreate
{
    [MenuItem("GameObject/BOXOPHOBIC/Atmospheric Height Fog/Global Volume", false, 9)]
    static void CreateGlobalVolume()
    {
        if (GameObject.Find("Height Fog Global") != null)
        {
            Debug.Log("[Warning][Atmospheric Height Fog] " + "Height Fog Global is already added to your scene!");
            return;
        }

        GameObject go = new GameObject();
        go.AddComponent<HeightFogGlobal>();

        Selection.activeGameObject = go;

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    [MenuItem("GameObject/BOXOPHOBIC/Atmospheric Height Fog/Override Volume", false, 9)]
    static void CreateOverrideVolume()
    {
        GameObject go = new GameObject();
        go.AddComponent<HeightFogOverride>();

        Selection.activeGameObject = go;

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}

