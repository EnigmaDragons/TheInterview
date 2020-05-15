using UnityEngine;

public class LongRangeInteractTrigger : MonoBehaviour
{
    [SerializeField] private OnTrigger trigger;

    public void Execute() => trigger.Trigger();
    public bool CanTrigger() => trigger.CanTrigger;
}