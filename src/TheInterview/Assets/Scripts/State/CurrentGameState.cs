using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "GameTemplate/OnlyOnce/CurrentGameState")]
public sealed class CurrentGameState : ScriptableObject
{
    [SerializeField] private GameState gameState;

    [SerializeField] private bool useDebugFields = true;
    [SerializeField] private List<string> debugTransientTriggers;
    [SerializeField] private List<string> debugPermanentTriggers;
    [SerializeField] private List<string> debugItems;

    public GameState ReadOnly => gameState;
    public AppState AppState => gameState.AppState;
    public Ending CurrentEnding => gameState.CurrentRunEnding;

    public void Init() => Init(new GameState());
    public void Init(GameState initialState) => UpdateState(gs => initialState);
    public void Refresh() => UpdateState(gs => { });
    public void SoftReset()
    {
        UpdateState(gs => gs.SoftReset());
        Message.Publish(new GameSoftResetted());
    }

    public void LockHud() => UpdateState(gs => gs.HudIsLocked = true);
    public void UnlockHud() => UpdateState(gs => gs.HudIsLocked = false);
    
    public void ToggleHud() => IfHudUnlocked(() => UpdateState(gs => gs.HudIsFocused = !gs.HudIsFocused));
    public void FocusHud() => IfHudUnlocked(() => UpdateState(gs => gs.HudIsFocused = true));
    public void DismissHud() => IfHudUnlocked(() => UpdateState(gs => gs.HudIsFocused = false));
    public void UnlockAndDismissHud() => UpdateState(gs =>
    {
        gs.HudIsLocked = false;
        gs.HudIsFocused = false;
    });
    
    private void IfHudUnlocked(Action a)
    {
        if (!gameState.HudIsLocked)
            a();
    }
    
    public void Subscribe(Action<GameStateChanged> onChange, object owner) => Message.Subscribe(onChange, owner);
    public void Unsubscribe(object owner) => Message.Unsubscribe(owner);

    public void SetAppViewAvailable(StringVariable viewName) => UpdateState(gs => gs.TransientTriggers.Add($"{viewName}-Activated"));
    public void SetAppViewCompleted(StringVariable viewName) => UpdateState(gs => gs.TransientTriggers.Add($"{viewName}-Completed"));
    public bool AppViewAvailable(StringVariable viewName)
        => gameState.TransientTriggers.Contains($"{viewName}-Activated") &&
           !gameState.TransientTriggers.Contains($"{viewName}-Completed");

    public void TransientTrigger(StringVariable triggerName) => UpdateState(gs => gs.TransientTriggers.Add(triggerName.Value));
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

        if (useDebugFields)
        {
            debugTransientTriggers = gameState.TransientTriggers.ToList();
            debugPermanentTriggers = gameState.PermanentTriggers.ToList();
            debugItems = gameState.InventoryItems.ToList();
        }

        Message.Publish(new GameStateChanged(gameState));
    }

    public bool ShouldBeHired => gameState.ShouldBeHired;
    public void FailedHiring() => gameState.ShouldBeHired = false;

    public Maybe<ObjectiveState> Objective => gameState.Objective;

    public void SetObjective(Objective objective)
    {
        UpdateState(x => x.Objective = new ObjectiveState(objective));
        Message.Publish(new ObjectiveGained());
    }

    public void FailObjective()
    {
        UpdateState(x =>
        {
            x.Objective.Value.Status = ObjectiveStatus.Failed;
            x.ResolvedObjectives.Add(x.Objective.Value);
        });
        Message.Publish(new ObjectiveFailed());
    }

    public void SucceedObjective()
    {
        UpdateState(x =>
        {
            x.Objective.Value.Status = ObjectiveStatus.Succeeded;
            x.ResolvedObjectives.Add(x.Objective.Value);
        });
        Message.Publish(new ObjectiveSucceeded(gameState.Objective.Value));
    }

    public void FailSubObjective(SubObjective subObjective)
    {
        UpdateState(state => state.Objective.Value.SubObjectives.First(x => x.SubObjective == subObjective).Status = ObjectiveStatus.Failed);
        Message.Publish(new SubObjectiveFailed());
    }

    public void SucceedSubObjective(SubObjective subObjective)
    {
        UpdateState(state => state.Objective.Value.SubObjectives.First(x => x.SubObjective == subObjective).Status = ObjectiveStatus.Succeeded);
        Message.Publish(new SubObjectiveSucceeded());
    }

    public void GainAchievement(Achievement achievement)
    {
        if (gameState.AchievedAchievements.Any(x => x.Achievement == achievement))
            return;
        UpdateState(x => x.AchievedAchievements.Add(new AchievementState { Achievement = achievement, HasPlayedSpeech = false }));
    }
}
