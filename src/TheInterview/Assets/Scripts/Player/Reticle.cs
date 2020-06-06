using UnityEngine;

public class Reticle : OnMessage<GameStateChanged, InteractionsPossible>
{
    [SerializeField] private GameObject reticle;

    private bool _hudIsFocused;
    private bool _interactionsPossible;

    private void Awake() => UpdateReticle();
    
    protected override void Execute(GameStateChanged msg)
    {
        _hudIsFocused = msg.State.HudIsFocused;
        UpdateReticle();
    }

    protected override void Execute(InteractionsPossible msg)
    {
        _interactionsPossible = msg.Any;
        UpdateReticle();
    }

    private void UpdateReticle() => reticle.SetActive( !_hudIsFocused && !_interactionsPossible);
}
