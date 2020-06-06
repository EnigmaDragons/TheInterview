using UnityEngine;

public class OnElevatorControlButtonsPressed : OnMessage<ElevatorControlButtonsPressed>
{
    [SerializeField] private Elevator elevator;
    [SerializeField] private TriggerData triggerData;

    protected override void Execute(ElevatorControlButtonsPressed msg)
    {
        if (msg.ElevatorId == elevator.GetInstanceID())
            triggerData.Trigger();
    }
}
