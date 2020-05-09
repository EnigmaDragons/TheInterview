using UnityEngine;

public class OnZoneEnter : MonoBehaviour
{
    [SerializeField] private OnTrigger trigger;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
            trigger.Trigger();
    }
}