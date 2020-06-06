using UnityEngine;

public class HudNotificationController : OnMessage<GameStateChanged, SendHudPrompt>
{
    [SerializeField] private GameObject notificationDisplay;

    private void Awake() => notificationDisplay.SetActive(false);

    protected override void Execute(GameStateChanged msg)
    {
        if (msg.State.HudIsFocused)
            notificationDisplay.SetActive(false);
    }

    protected override void Execute(SendHudPrompt msg) => notificationDisplay.SetActive(true);
}
