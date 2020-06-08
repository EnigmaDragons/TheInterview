using UnityEngine;

public class OnHacked : OnMessage<GameStateChanged>
{
    [SerializeField] private CurrentGameState currentGameState;
    [SerializeField] private StringReference hackedKey;
    [SerializeField] private Trigger trigger;

    private bool _hasBeenHacked;

    private void Awake() => _hasBeenHacked = currentGameState.HasTriggeredThisRun(hackedKey.Value);

    protected override void Execute(GameStateChanged msg)
    {
        if (!_hasBeenHacked && currentGameState.HasTriggeredThisRun(hackedKey.Value))
        {
            _hasBeenHacked = true;
            trigger.Execute();
        }
    }
}