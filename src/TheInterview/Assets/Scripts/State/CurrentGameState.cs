using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameTemplate/OnlyOnce/CurrentGameState")]
public sealed class CurrentGameState : ScriptableObject
{
    [SerializeField] private GameState gameState;

    public AppState AppState => gameState.AppState;
    public Ending CurrentEnding => gameState.CurrentRunEnding;

    public void Init() => Init(new GameState());
    public void Init(GameState initialState) => UpdateState(gs => initialState);
    public void SoftReset() => UpdateState(gs => gs.SoftReset());
    
    public void LockHud() => UpdateState(gs => gs.HudIsLocked = true);
    public void UnlockHud() => UpdateState(gs => gs.HudIsLocked = false);

    public void ToggleHud() => IfHudUnlocked(() => UpdateState(gs => gs.HudIsFocused = !gs.HudIsFocused));
    public void FocusHud() => IfHudUnlocked(() => UpdateState(gs => gs.HudIsFocused = true));
    public void DismissHud() => IfHudUnlocked(() => UpdateState(gs => gs.HudIsFocused = false));
    private void IfHudUnlocked(Action a)
    {
        if (!gameState.HudIsLocked)
            a();
    }
    
    public void Subscribe(Action<GameStateChanged> onChange, object owner) => Message.Subscribe(onChange, owner);
    public void Unsubscribe(object owner) => Message.Unsubscribe(owner);

    public void SetAppViewAvailable(StringVariable viewName) => gameState.TransientTriggers.Add($"{viewName}-Activated");
    public void SetAppViewCompleted(StringVariable viewName) => gameState.TransientTriggers.Add($"{viewName}-Completed");
    public bool AppViewAvailable(StringVariable viewName)
        => gameState.TransientTriggers.Contains($"{viewName}-Activated") &&
           !gameState.TransientTriggers.Contains($"{viewName}-Completed");

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

    public Maybe<ObjectiveState> Objective => gameState.Objective;

    public void SetObjective(Objective objective)
    {
        UpdateState(x => x.Objective = new Maybe<ObjectiveState>(new ObjectiveState(objective)));
        Message.Publish(new ObjectiveGained());
    }

    public void FailObjective()
    {
        UpdateState(x => x.Objective.Value.Status = ObjectiveStatus.Failed);
        Message.Publish(new ObjectiveFailed());
    }

    public void SucceedObjective()
    {
        UpdateState(x => x.Objective.Value.Status = ObjectiveStatus.Succeeded);
        Message.Publish(new ObjectiveSucceeded());
    }

    public void FailSubObjective(SubObjective subObjective)
    {
        UpdateState(x => x.Objective.Value.Status = ObjectiveStatus.Failed);
        Message.Publish(new SubObjectiveFailed());
    }

    public void SucceedSubObjective(SubObjective subObjective)
    {
        UpdateState(x => x.Objective.Value.Status = ObjectiveStatus.Succeeded);
        Message.Publish(new SubObjectiveSucceeded());
    }
}
