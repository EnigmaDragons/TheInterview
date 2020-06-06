using UnityEngine;

public class HudController : OnMessage<GameStateChanged>
{
    [SerializeField] private HudAppView[] views;

    protected override void Execute(GameStateChanged msg)
    {
        if (msg.State.HudIsFocused) 
            views.ForEach(v => v.EnableIfActivated());
    }
}
