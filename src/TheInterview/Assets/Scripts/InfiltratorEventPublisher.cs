
using UnityEngine;

[CreateAssetMenu(menuName = "Only Once/InfiltratorEventPublisher")]
public class InfiltratorEventPublisher : ScriptableObject
{
    public void SendHudPrompt(string type = "Notification") => Message.Publish(new SendHudPrompt { PromptType = type });
    public void GainItem(Item item) => Message.Publish(new GainItem(item));
}
