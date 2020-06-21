using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private TriggerData defaultTrigger;
    [SerializeField] private TriggerSwitchOption[] triggers;

    private bool _hasTriggered = false;

    private void Awake() => _hasTriggered = false;

    public void Execute()
    {
        foreach (var triggerSwitchOption in triggers)
        {
            if (gameState.HasTriggeredThisRun(triggerSwitchOption.Condition))
            {
                TriggerIfPossible(triggerSwitchOption.TriggerData);
                return;
            }
        }
        TriggerIfPossible(defaultTrigger);
    }

    private void OnTriggerEnter(Collider c)
    {
        foreach (var triggerSwitchOption in triggers)
        {
            if (gameState.HasTriggeredThisRun(triggerSwitchOption.Condition) && triggerSwitchOption.TriggerData.InteractType == TriggerInteractType.Enter && c.CompareTag("Player"))
            {
                TriggerIfPossible(triggerSwitchOption.TriggerData);
                return;
            }
        }
        if (defaultTrigger.InteractType == TriggerInteractType.Enter && c.CompareTag("Player"))
            TriggerIfPossible(defaultTrigger);
    }

    public bool CanTrigger(TriggerData trigger)
    {
        var baseCanTrigger = trigger.CanTrigger;
        if (_hasTriggered && trigger.Repeatability == TriggerRepeatability.OncePerObject)
            return false;
        if (baseCanTrigger && !_hasTriggered)
            return true;
        if (baseCanTrigger && trigger.Repeatability == TriggerRepeatability.RepeatForever)
            return true;
        return true;
    }

    private void TriggerIfPossible(TriggerData trigger)
    {
        if (!CanTrigger(trigger))
            return;

        trigger.Trigger();
        _hasTriggered = true;
    }
}