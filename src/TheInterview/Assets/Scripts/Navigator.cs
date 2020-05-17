
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Only Once/Navigator")]
public class Navigator : ScriptableObject
{
    public void GoToShortGame() => LoadScene("ShortGame-FullScene");
    public void GoToMainMenu() => LoadScene("MainMenu");
    public void GoToIntroCutscene() => LoadScene("SimpleIntroCutscene");
    public void GoToCredits() => LoadScene("SimpleCreditsScene");
    public void GoToScene2() => LoadScene("Scene2-UrbanamicsBuilding");
    public void GoToScene3() => LoadScene("Scene3-TheInterview");
    public void GoToApartment() => LoadScene("Scene1-Apartment");
    public void DriveToBuilding() => LoadScene("Cutscene-DrivingToInterview");
    public void StartGame() => GoToApartment();    
    
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
