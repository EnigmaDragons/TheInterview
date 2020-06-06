using UnityEngine;

public class Reticle : OnMessage<GameStateChanged, InteractionsPossible>
{
    [SerializeField] private GameObject reticle;

    private GameState _state;
    private bool _interactionsPossible;

    private void Awake() => UpdateReticle();
    
    protected override void Execute(GameStateChanged msg)
    {
        _state = msg.State;
        UpdateReticle();
    }

    protected override void Execute(InteractionsPossible msg)
    {
        _interactionsPossible = msg.Any;
        UpdateReticle();
    }

    private void UpdateReticle() => reticle.SetActive(!_state.HudIsFocused && !_interactionsPossible);
}
