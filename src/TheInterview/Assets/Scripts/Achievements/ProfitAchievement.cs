using System.Linq;
using UnityEngine;

public class ProfitAchievement : OnMessage<ObjectiveSucceeded>
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private Objective[] allObjectives;
    [SerializeField] private Achievement profitAchievement;

    protected override void Execute(ObjectiveSucceeded msg)
    {
        var fullyCompletedObjectives = gameState.ReadOnly.ResolvedObjectives.Where(obj =>
            obj.Status == ObjectiveStatus.Succeeded && obj.SubObjectives.All(subObj => subObj.Status == ObjectiveStatus.Succeeded)).ToArray();
        if (allObjectives.All(obj => fullyCompletedObjectives.Any(completedObj => completedObj.Objective == obj)))
            gameState.GainAchievement(profitAchievement);
    }
}