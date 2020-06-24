using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProfitAchievement : CrossSceneSingleInstance
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private Objective[] allObjectives;
    [SerializeField] private Achievement profitAchievement;

    private List<Objective> _objectivesFullyCompleted = new List<Objective>();

    private void OnEnable()
    {
        Message.Subscribe<ObjectiveSucceeded>(Execute, this);
        Message.Subscribe<GameSoftResetted>(x => _objectivesFullyCompleted = new List<Objective>(), this);
    }

    private void OnDisable() => Message.Unsubscribe(this);

    private void Execute(ObjectiveSucceeded msg)
    {
        if (msg.Objective.SubObjectives.All(x => x.Status == ObjectiveStatus.Succeeded))
            _objectivesFullyCompleted.Add(msg.Objective.Objective);
        if (allObjectives.All(x => _objectivesFullyCompleted.Contains(x)))
            gameState.GainAchievement(profitAchievement);
    }

    protected override string UniqueTag => "Achievement";
    protected override void OnAwake() {}
}