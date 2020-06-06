using UnityEngine;
using UnityEngine.Events;

public class OnAppInstalled : OnMessage<AppStateChanged>
{
    [SerializeField] private UnityEvent action;

    private bool _finished;
    
    protected override void Execute(AppStateChanged msg)
    {
        if (_finished || !msg.AppState.AppInstalled)
            return;

        _finished = true;
        action.Invoke();
    }
}
