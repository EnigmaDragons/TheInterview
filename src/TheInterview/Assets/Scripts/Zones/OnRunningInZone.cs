using UnityEngine;

public class OnRunningInZone : OnMessage<PlayerMoveStateStarted, PlayerMoveStateEnded>
{
    [SerializeField] private OnTrigger trigger;

    private bool isRunning = false;
    private bool isPlayerIn = false;

    protected override void Execute(PlayerMoveStateStarted msg)
    {
        if (msg.State == PlayerMoveState.Running)
            isRunning = true;
        IfApplicableTrigger();
    }

    protected override void Execute(PlayerMoveStateEnded msg)
    {
        if (msg.State == PlayerMoveState.Running)
            isRunning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            isPlayerIn = true;
        IfApplicableTrigger();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
            isPlayerIn = true;
        IfApplicableTrigger();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            isPlayerIn = false;
    }

    private void IfApplicableTrigger()
    {
        if (isPlayerIn && isRunning)
            trigger.Trigger();
    }
}