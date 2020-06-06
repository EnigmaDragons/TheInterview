﻿using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Only Once/InfiltratorNavigator")]
public class InfiltratorNavigator : ScriptableObject
{
    public void GoToMainMenu() => LoadScene("InfiltratorMainMenu");
    public void GoToStartingApartment() => LoadScene("InfiltratorApartment");
    public void GoToServerFarmExterior() => LoadScene("InfiltratorServerFarmExterior");
    public void GoToServerFarmInterior() => LoadScene("InfiltratorServerFarmInterior");
    public void StartGame() => GoToStartingApartment();    
    
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}