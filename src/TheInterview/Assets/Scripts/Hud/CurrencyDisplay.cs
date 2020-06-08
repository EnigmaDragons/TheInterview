using TMPro;
using UnityEngine;

public class CurrencyDisplay : OnMessage<AppStateChanged>
{
    [SerializeField] private CurrentAppState appState;
    [SerializeField] private TextMeshProUGUI display;
    [SerializeField] private UiSfxPlayer sfx;
    [SerializeField] private AudioClipVolume moneySound;

    private void Start() => UpdateDisplay(appState.State.Creds);
    protected override void Execute(AppStateChanged msg) => UpdateDisplay(msg.AppState.Creds);
    
    private void UpdateDisplay(int amount)
    {
        display.text = amount.ToString();
        if (amount > 0)
            sfx.Play(moneySound);
    }
}
