
using UnityEngine;

public sealed class Trigger : MonoBehaviour
{
    [SerializeField] private TriggerData triggerData;
    
    private bool _hasTriggered = false;

    private void Awake() => _hasTriggered = false;
    
    public void Execute()
    {
        if (triggerData.InteractType == TriggerInteractType.Interact)
            TriggerIfPossible();
    }
    
    private void OnTriggerEnter(Collider c)
    {
        if (triggerData.InteractType == TriggerInteractType.Enter && c.CompareTag("Player"))
            TriggerIfPossible();
    }

    public bool CanTrigger()
    {
        var baseCanTrigger = triggerData.CanTrigger;
        if (_hasTriggered && triggerData.Repeatability == TriggerRepeatability.OncePerObject)
            return false;
        if (baseCanTrigger && !_hasTriggered)
            return true;
        if (baseCanTrigger && triggerData.Repeatability == TriggerRepeatability.RepeatForever)
            return true;
        return true; 
    }

    private void TriggerIfPossible()
    {
        if (!CanTrigger()) 
            return;
        
        triggerData.Trigger();
        _hasTriggered = true;
    }
}
