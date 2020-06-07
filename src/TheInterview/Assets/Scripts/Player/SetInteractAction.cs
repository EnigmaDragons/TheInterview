
using UnityEngine;
using UnityEngine.Events;

public class SetInteractAction : MonoBehaviour
{
    [SerializeField] private InteractAction target;
    [SerializeField] private UnityEvent action;

    private void Awake() => target.Add(action);
}
