
using UnityEngine;

[CreateAssetMenu(menuName = "Only Once/Event Publisher")]
public class EventPublisher : ScriptableObject
{
    public void ResetGameAfterDelay() => Message.Publish(new ResetGameAfterDelay());
    public void WeaknessSelected() => Message.Publish(new WeaknessSelected());
    public void WeaknessSelectedFinished() => Message.Publish(new Finished<WeaknessSelected>());
}
