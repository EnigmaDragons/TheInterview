﻿
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Only Once/Navigator")]
public class Navigator : ScriptableObject
{
    public void GoToShortGame() => LoadScene("ShortGame-FullScene");
    public void GoToScene2() => LoadScene("Scene2-UrbanamicsBuilding");
    public void GoToScene3() => LoadScene("Scene3-TheInterview");

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
