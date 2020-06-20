using UnityEngine;

public class HudIdScanAppController : OnMessage<BeginIdScan>
{
    [SerializeField] private IdScanApp app;
    [SerializeField] private HudAppView appView;
    
    protected override void Execute(BeginIdScan msg)
    {
        app.Init(msg.Requirement);
        Message.Publish(new LaunchApp(appView));
    }
}
