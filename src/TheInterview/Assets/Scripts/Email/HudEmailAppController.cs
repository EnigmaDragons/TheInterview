using UnityEngine;

public sealed class HudEmailAppController : OnMessage<ShowEmail>
{
    [SerializeField] private EmailApp app;

    protected override void Execute(ShowEmail msg)
    {
        app.Init(msg.Email);
        Message.Publish(new LaunchApp(app));
    }
}
