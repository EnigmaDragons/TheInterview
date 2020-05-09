using UnityEngine;

public class OnElevatorControlButtonsPressed : OnMessage<ElevatorControlButtonsPressed>
{
    [SerializeField] private Elevator elevator;
    [SerializeField] private OnTrigger onTrigger;

    protected override void Execute(ElevatorControlButtonsPressed msg)
    {
        if (msg.ElevatorId == elevator.GetInstanceID())
            onTrigger.Trigger();
    }
}
