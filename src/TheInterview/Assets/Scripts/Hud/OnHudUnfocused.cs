using UnityEngine;
using UnityEngine.Events;

public class OnHudUnfocused : OnMessage<GameStateChanged>
{
    [SerializeField] private UnityEvent action;

    private bool _wasActivated;
    
    protected override void Execute(GameStateChanged msg)
    {
        if (msg.State.HudIsFocused)
            _wasActivated = true;
        
        if (!_wasActivated || msg.State.HudIsFocused)
            return;
        
        action.Invoke();
        enabled = false;
    }
}