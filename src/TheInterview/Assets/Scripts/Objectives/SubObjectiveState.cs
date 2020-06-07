using System;

[Serializable]
public class SubObjectiveState
{
    public SubObjective SubObjective;
    public ObjectiveStatus Status;

    public SubObjectiveState() {}
    public SubObjectiveState(SubObjective subObjective)
    {
        SubObjective = subObjective;
        Status = ObjectiveStatus.Uncompleted;
    }
}