using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GameTemplate/OnlyOnce/CurrentGameState")]
public sealed class CurrentGameState : ScriptableObject
{
    [SerializeField] private GameState gameState;
    [SerializeField] private IntVariable[] counters;
    [SerializeField] private IntVariable[] softCounters;

    public void Init()
    {
        SoftReset();
        for (var i = 0; i < counters.Length; i++)
            counters[i].Value = 0;
        gameState = new GameState();
    }

    public void Init(GameState initialState) => gameState = initialState;
    public void SoftReset()
    {
        for (var i = 0; i < softCounters.Length; i++)
            softCounters[i].Value = 0;
    }
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
}
