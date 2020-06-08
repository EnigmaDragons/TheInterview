using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class ObjectiveState
{
    public Objective Objective;
    public ObjectiveStatus Status;
    public List<SubObjectiveState> SubObjectives;

    public ObjectiveState() {}
    public ObjectiveState(Objective objective)
    {
        Objective = objective;
        Status = ObjectiveStatus.Uncompleted;
        SubObjectives = objective.SubObjectives.Select(x => new SubObjectiveState(x)).ToList();
    }

    private IEnumerable<SubObjective> SuccessfulSubs 
        => SubObjectives
            .Where(s => s.Status == ObjectiveStatus.Succeeded)
            .Select(s => s.SubObjective);
    
    private IEnumerable<SubObjective> FailedSubs 
        => SubObjectives
            .Where(s => s.Status == ObjectiveStatus.Failed)
            .Select(s => s.SubObjective);
    
    public int RewardCredsAmount 
        => Objective.Creds 
           + SuccessfulSubs.Sum(s => s.RewardCreds) 
           - FailedSubs.Sum(s => s.PenaltyCreds);

    public int RewardXpAmount
        => Objective.XP
           + SuccessfulSubs.Sum(s => s.RewardXP)
           - FailedSubs.Sum(s => s.PenaltyXP);
}