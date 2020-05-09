
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Only Once/Navigator")]
public class Navigator : ScriptableObject
{
    public void GoToScene2() => LoadScene("Scene2-UrbanamicsBuilding");

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
