using UnityEngine;

public class HudAppView : MonoBehaviour
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private StringVariable appViewName;

    public void EnableIfActivated() => gameObject.SetActive(ShouldBeEnabled());
    public bool ShouldBeEnabled() => game.AppViewAvailable(appViewName);
}
