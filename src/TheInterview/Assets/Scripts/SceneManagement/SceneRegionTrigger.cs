
using UnityEngine;

public class SceneRegionTrigger : TriggerZone
{
    [SerializeField] private string enableRegion;
    [SerializeField] private string disableRegion;

    void Awake() => Action = SendMessages;

    private void SendMessages()
    {
        if (!string.IsNullOrWhiteSpace(enableRegion))
            Message.Publish(new EnableRegion { RegionName = enableRegion });
        if (!string.IsNullOrWhiteSpace(disableRegion))
            Message.Publish(new DisableRegion { RegionName = disableRegion });
    }
}
