using UnityEngine;

public class HudController : OnMessage<GameStateChanged, LaunchApp>
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private HudAppView[] views;

    private bool _wasFocused = false;
    private Maybe<HudAppView> _current = new Maybe<HudAppView>();

    protected override void Execute(GameStateChanged msg)
    {
        if (_wasFocused == msg.State.HudIsFocused)
            return;
        
        _wasFocused = msg.State.HudIsFocused;
        if (msg.State.HudIsFocused)
            foreach (var v in views)
                if (v.ShouldBeEnabled())
                    ActivateProgram(v);
        if (!msg.State.HudIsFocused)
            CloseProgram();
    }

    protected override void Execute(LaunchApp msg)
    {
        game.UpdateState(gs => gs.HudIsFocused = true);
        ActivateProgram(msg.App);
    }

    private void CloseProgram()
    {
        if (_current.IsMissing)
            return;
        
        _current.Value.gameObject.SetActive(false);
        if (_current.Value.IsRequired)
            game.SetAppViewCompleted(_current.Value.ViewName);
        _current = new Maybe<HudAppView>();
    }

    private void ActivateProgram(HudAppView v)
    {
        if (_current.IsPresent)
            CloseProgram();
        
        _current = v;
        v.gameObject.SetActive(true);
        if (v.IsRequired)
            game.UpdateState(gs => gs.HudIsLocked = true);
    }
}
