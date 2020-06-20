using UnityEngine;

public class HudYesNoSurveyAppController : OnMessage<BeginYesNoSurvey>
{
    [SerializeField] private YesNoSurveyApp yesNoApp;
    [SerializeField] private HudAppView appView;
    
    protected override void Execute(BeginYesNoSurvey msg)
    {
        yesNoApp.Init(msg.Question);
        Message.Publish(new LaunchApp(appView));
    }
}
