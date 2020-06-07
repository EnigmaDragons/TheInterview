
using UnityEngine;

public class SetEnabledBasedOnGameTrigger : OnMessage<GameStateChanged>
{
    [SerializeField] private GameObject target;
    [SerializeField] private StringReference trigger;
    [SerializeField] private bool shouldBeEnabledIfTriggered = true;
    
    protected override void Execute(GameStateChanged msg)
    {
        var isTriggered = msg.State.IsTriggered(trigger.Value);
        target.SetActive(shouldBeEnabledIfTriggered ? isTriggered : !isTriggered);
    }
}
