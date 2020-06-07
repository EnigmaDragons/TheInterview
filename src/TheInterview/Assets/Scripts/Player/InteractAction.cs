
using UnityEngine;
using UnityEngine.Events;

public class InteractAction : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    public void Add(UnityEvent e) => action.AddListener(e.Invoke);
    
    public void Execute() => action.Invoke();
}
