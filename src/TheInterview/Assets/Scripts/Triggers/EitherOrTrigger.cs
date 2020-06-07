using UnityEngine;

public class EitherOrTrigger : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private string hasPlayedTrigger;
    [SerializeField] private TriggerData trueTriggerData;
    [SerializeField] private TriggerData falseTriggerData;

    private bool _hasTriggered = false;

    private void Awake() => _hasTriggered = false;

    public void Execute()
    {
        if (gameState.HasTriggeredThisRun(hasPlayedTrigger))
        {
            if (trueTriggerData.InteractType == TriggerInteractType.Interact)
                TriggerIfPossible(trueTriggerData);
        }
        else
        {
            if (falseTriggerData.InteractType == TriggerInteractType.Interact)
                TriggerIfPossible(falseTriggerData);
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (gameState.HasTriggeredThisRun(hasPlayedTrigger))
        {
            if (trueTriggerData.InteractType == TriggerInteractType.Enter && c.CompareTag("Player"))
                TriggerIfPossible(trueTriggerData);
        }
        else
        {
            if (falseTriggerData.InteractType == TriggerInteractType.Enter && c.CompareTag("Player"))
                TriggerIfPossible(falseTriggerData);
        }
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
