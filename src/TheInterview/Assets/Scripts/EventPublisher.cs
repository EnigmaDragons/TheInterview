
using UnityEngine;

[CreateAssetMenu(menuName = "Only Once/Event Publisher")]
public class EventPublisher : ScriptableObject
{
    public void ResetGameAfterDelay() => Message.Publish(new ResetGameAfterDelay());
    public void WeaknessSelected(string name = "") => Message.Publish(new WeaknessSelected { Name = name });
    public void WeaknessSelectedFinished(string name = "") => Message.Publish(new Finished<WeaknessSelected> { Message = new WeaknessSelected { Name = name }});
    public void HideOrbs() => Message.Publish(new HideOrbs());
    public void PresentNextQuestion() => Message.Publish(new PresentNextQuestion());
    public void PresentAnswers() => Message.Publish(new PresentAnswers());
}
