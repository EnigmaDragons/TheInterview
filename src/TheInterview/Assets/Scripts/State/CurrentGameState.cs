using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(menuName = "GameTemplate/OnlyOnce/CurrentGameState")]
public sealed class CurrentGameState : ScriptableObject
{
    [SerializeField] private GameState gameState;

    public void Init() => Init(new GameState());
    public void Init(GameState initialState) => UpdateState(gs => initialState);
    public void SoftReset() => UpdateState(gs => gs.SoftReset());

    public void Subscribe(Action<GameStateChanged> onChange, object owner) => Message.Subscribe(onChange, owner);
    public void Unsubscribe(object owner) => Message.Unsubscribe(owner);
    
    public void UpdateState(Action<GameState> apply)
    {
        UpdateState(_ =>
        {
            apply(gameState);
            return gameState;
        });
    }
    
    public void UpdateState(Func<GameState, GameState> apply)
    {
        gameState = apply(gameState);
        Message.Publish(new GameStateChanged(gameState));
    }

    public bool ShouldBeHired => gameState.ShouldBeHired;
    public void FailedHiring() => gameState.ShouldBeHired = false;
}
