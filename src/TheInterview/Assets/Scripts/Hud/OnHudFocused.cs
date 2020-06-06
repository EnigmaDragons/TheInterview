using UnityEngine;
using UnityEngine.Events;

public class OnHudFocused : OnMessage<GameStateChanged>
{
    [SerializeField] private UnityEvent action;
    
    protected override void Execute(GameStateChanged msg)
    {
        if (!msg.State.HudIsFocused)
            return;
        
        action.Invoke();
        enabled = false;
    }
}
