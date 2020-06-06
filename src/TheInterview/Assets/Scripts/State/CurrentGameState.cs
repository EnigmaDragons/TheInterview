using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(menuName = "GameTemplate/OnlyOnce/CurrentGameState")]
public sealed class CurrentGameState : ScriptableObject
{
    [SerializeField] private GameState gameState;

    public AppState AppState => gameState.AppState;

    public void Init() => Init(new GameState());
    public void Init(GameState initialState) => UpdateState(gs => initialState);
    public void SoftReset() => UpdateState(gs => gs.SoftReset());

    public void ToggleHud() => UpdateState(gs => gs.HudIsFocused = !gs.HudIsFocused);
    public void FocusHud() => UpdateState(gs => gs.HudIsFocused = true);
    public void DismissHud() => UpdateState(gs => gs.HudIsFocused = false);
    
    public void Subscribe(Action<GameStateChanged> onChange, object owner) => Message.Subscribe(onChange, owner);
    public void Unsubscribe(object owner) => Message.Unsubscribe(owner);

    public bool HasTriggeredThisRun(string counterName) => gameState.TransientTriggers.Contains(counterName);
    
    public void IncrementCounter(TriggerStateLifecycle lifecycle, string counterName)
    {
        Counters(lifecycle)[counterName] = Counter(lifecycle, counterName) + 1;
        gameState.TransientTriggers.Add(counterName);
    }

    public int Counter(TriggerStateLifecycle lifecycle, string counterName) 
        => Counters(lifecycle).TryGetValue(counterName, out var val) ? val : 0;

    private IDictionary<string, int> Counters(TriggerStateLifecycle lifecycle) 
        => lifecycle == TriggerStateLifecycle.Permanent
            ? gameState.PermanentCounters
            : (IDictionary<string, int>)gameState.TransientCounters;
    
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
