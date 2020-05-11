
using UnityEngine;
using UnityEngine.Events;

public class OnEnableAction : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    void OnEnable() => action.Invoke();
}
