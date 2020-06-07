using UnityEngine;

public class HudHackAppController : OnMessage<BeginKeypadEntry>
{
    [SerializeField] private KeypadEntryProgram program;
    [SerializeField] private HudAppView app;
    
    protected override void Execute(BeginKeypadEntry msg)
    {
        program.Init(msg.Secret);
        Message.Publish(new LaunchApp(app));
    }
}
