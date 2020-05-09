
using UnityEngine;

[CreateAssetMenu(menuName = "Only Once/Event Publisher")]
public class EventPublisher : ScriptableObject
{
    public void ResetGameAfterDelay() => Message.Publish(new ResetGameAfterDelay());
}
