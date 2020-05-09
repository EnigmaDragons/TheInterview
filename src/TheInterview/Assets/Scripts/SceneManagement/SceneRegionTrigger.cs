
using UnityEngine;

public class SceneRegionTrigger : TriggerZone
{
    [SerializeField] private SceneRegion enableRegion;
    [SerializeField] private SceneRegion disableRegion;

    void Awake() => Action = SendMessages;

    private void SendMessages()
    {
        Message.Publish(new EnableRegion { Region = enableRegion });
        Message.Publish(new DisableRegion { Region = disableRegion });
    }
}
