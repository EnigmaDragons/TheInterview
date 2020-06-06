
using UnityEngine;
using UnityEngine.Events;

public class InteractAction : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    public void Execute() => action.Invoke();
}
