
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    [SerializeField] private OnTrigger trigger;

    public void Execute() => trigger.Trigger();
    public bool CanTrigger() => trigger.CanTrigger;
}
