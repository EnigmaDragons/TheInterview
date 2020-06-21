using UnityEngine;

public class HudDigitalPackageAppController : OnMessage<SendItem>
{
    [SerializeField] private DigitalPackageApp app;
    
    protected override void Execute(SendItem msg)
    {
        app.Init(msg.Item);
        Message.Publish(new LaunchApp(app));
    }
}
