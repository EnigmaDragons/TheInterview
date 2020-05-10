
using UnityEngine;

public class SceneRegionTrigger : TriggerZone
{
    [SerializeField] private string enableRegion;
    [SerializeField] private string disableRegion;

    void Awake() => Action = SendMessages;

    private void SendMessages()
    {
        Message.Publish(new EnableRegion { RegionName = enableRegion });
        Message.Publish(new DisableRegion { RegionName = disableRegion });
    }
}
