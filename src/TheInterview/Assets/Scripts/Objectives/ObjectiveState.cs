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
}