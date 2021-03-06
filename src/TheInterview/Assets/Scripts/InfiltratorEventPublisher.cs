﻿
using UnityEngine;

[CreateAssetMenu(menuName = "Only Once/InfiltratorEventPublisher")]
public class InfiltratorEventPublisher : ScriptableObject
{
    public void SendHudPrompt(string type = "Notification") => Message.Publish(new SendHudPrompt { PromptType = type });
    public void GainItem(Item item) => Message.Publish(new GainItem(item));
    public void RemoveItem(Item item) => Message.Publish(new RemoveItem(item.Name));
    public void PlayEnding(Ending ending) => Message.Publish(new PlayEnding(ending));
    public void StartKeyCodeEntry(CodeHackSecret secret) => Message.Publish(new BeginKeypadEntry(secret));
    public void RefuseAppInstall() => Message.Publish(new AppInstallRefused());
    public void FinishRefuseAppInstall() => Message.Publish(new Finished<AppInstallRefused>());
    public void StartTimer(TimerData d) => Message.Publish(new StartTimer(d));
    public void StopTimer() => Message.Publish(new StopTimer());
    public void AskSurveyQuestion(YesNoSurveyQuestion q) => Message.Publish(new BeginYesNoSurvey(q));
    public void ActivateIdScan(IdAccessRequirement r) => Message.Publish(new BeginIdScan(r));
    public void StartFire() => Message.Publish(new StartFire());
    public void ShowEmail(Email e) => Message.Publish(new ShowEmail(e));
    public void SendItemDigitally(Item item) => Message.Publish(new SendItem(item));
    public void EndingSpeechFinished() => Message.Publish(new EndingSpeechFinished());
}
