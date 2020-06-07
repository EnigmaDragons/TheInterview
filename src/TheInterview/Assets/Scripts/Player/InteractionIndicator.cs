using System;
using UnityEngine;

public sealed class InteractionIndicator : OnMessage<InteractionsPossible, GameStateChanged>
{
    [SerializeField] private GameObject indicator;

    private bool _anyInteractions = false;
    private bool _isInRealWorld = true;

    private void Awake() => indicator.SetActive(false);

    protected override void Execute(InteractionsPossible msg) 
        => UpdateIndicatorAfter(() => _anyInteractions = msg.Any);
    
    protected override void Execute(GameStateChanged msg) 
        => UpdateIndicatorAfter(() => _isInRealWorld = !msg.State.HudIsFocused);

    private void UpdateIndicatorAfter(Action update)
    {
        update();
        indicator.SetActive(_isInRealWorld && _anyInteractions);
    }
}
