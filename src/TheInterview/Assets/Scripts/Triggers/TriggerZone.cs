using UnityEngine;
using UnityEngine.Events;

public abstract class TriggerZone : MonoBehaviour
{
    [SerializeField] protected UnityAction Action;

    public void OnCollisionEnter(Collision other) => Execute(other.gameObject);
    public void OnTriggerEnter(Collider other) => Execute(other.gameObject);
    private void Execute(GameObject other) => Action.Invoke();
}
