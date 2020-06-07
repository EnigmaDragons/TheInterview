﻿
using UnityEngine;

[CreateAssetMenu(menuName = "Only Once/InfiltratorEventPublisher")]
public class InfiltratorEventPublisher : ScriptableObject
{
    public void SendHudPrompt(string type = "Notification") => Message.Publish(new SendHudPrompt { PromptType = type });
    public void GainItem(Item item) => Message.Publish(new GainItem(item));
    public void PlayEnding(Ending ending) => Message.Publish(new PlayEnding(ending));
    public void StartKeyCodeEntry(CodeHackSecret secret) => Message.Publish(new BeginKeypadEntry(secret));
}
