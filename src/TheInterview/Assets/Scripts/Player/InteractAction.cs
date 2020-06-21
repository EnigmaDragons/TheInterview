
using UnityEngine;
using UnityEngine.Events;

public class InteractAction : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    private int _numAddedActions;
    
    public void Add(UnityEvent e)
    {
        action.AddListener(e.Invoke);
        _numAddedActions++;
    }

    public void Execute() => action.Invoke();

    private void Start()
    {
        if (_numAddedActions + action.GetPersistentEventCount() < 1)
            enabled = false;
    }
}
