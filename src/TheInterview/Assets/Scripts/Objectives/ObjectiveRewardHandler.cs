using UnityEngine;

public class ObjectiveRewardHandler : OnMessage<ObjectiveSucceeded>
{
    [SerializeField] private CurrentAppState app;
    
    protected override void Execute(ObjectiveSucceeded msg)
    {
        app.UpdateState(a =>
        {
            a.Creds += msg.Objective.RewardCredsAmount;
            a.CurrentLevelXp += msg.Objective.RewardXpAmount;
        });
    }
}
