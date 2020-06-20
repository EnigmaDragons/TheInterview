using UnityEngine;

public class OnHackFailed : OnMessage<HackFailed>
{
    [SerializeField] private StringReference deviceId;
    [SerializeField] private Trigger trigger;

    protected override void Execute(HackFailed msg)
    {
        if (msg.DeviceId == deviceId.Value)
            trigger.Execute();
    }
}