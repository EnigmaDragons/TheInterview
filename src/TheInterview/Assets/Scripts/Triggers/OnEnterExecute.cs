using UnityEngine;
using UnityEngine.Events;

public class OnEnterExecute : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    public void OnCollisionEnter(Collision other) => Execute(other.gameObject);
    public void OnTriggerEnter(Collider other) => Execute(other.gameObject);
    private void Execute(GameObject other) => action.Invoke();
}
