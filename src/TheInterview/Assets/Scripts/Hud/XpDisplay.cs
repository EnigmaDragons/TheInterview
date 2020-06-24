using TMPro;
using UnityEngine;

public class XpDisplay : OnMessage<AppStateChanged>
{
    [SerializeField] private CurrentAppState appState;
    [SerializeField] private TextMeshProUGUI display;

    private void Start() => UpdateDisplay(appState.State);
    protected override void Execute(AppStateChanged msg) => UpdateDisplay(msg.AppState);
    
    private void UpdateDisplay(AppState a)
    {
        display.text = $"{a.CurrentLevelXp}/{a.NextLevelXp}";
    }
}
