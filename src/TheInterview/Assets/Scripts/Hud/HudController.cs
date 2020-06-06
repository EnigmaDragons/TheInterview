using UnityEngine;

public class HudController : OnMessage<GameStateChanged>
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private HudAppView[] views;

    private bool _wasFocused = false;
    private HudAppView _current;

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

    private void CloseProgram()
    {
        if (_current == null)
            return;
        
        _current.gameObject.SetActive(false);
        if (_current.IsRequired)
            game.SetAppViewCompleted(_current.ViewName);
        _current = null;
    }

    private void ActivateProgram(HudAppView v)
    {
        _current = v;
        v.gameObject.SetActive(true);
        if (v.IsRequired)
            game.UpdateState(gs => gs.HudIsLocked = true);
    }
}
