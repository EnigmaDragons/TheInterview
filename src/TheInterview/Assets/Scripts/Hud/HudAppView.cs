using UnityEngine;

public class HudAppView : MonoBehaviour
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private StringVariable appViewName;
    [SerializeField] private bool isRequired = true;

    public bool IsRequired => isRequired;
    public StringVariable ViewName => appViewName;
    
    public bool ShouldBeEnabled() => !gameObject.activeSelf && game.AppViewAvailable(appViewName);
}
