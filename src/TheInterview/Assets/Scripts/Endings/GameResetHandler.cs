using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameResetHandler : OnMessage<ResetGameAfterDelay>
{
    [SerializeField] private FloatReference delay;
    [SerializeField] private Navigator navigator;
    [SerializeField] private UnityEvent initialAction;
    
    protected override void Execute(ResetGameAfterDelay msg) => StartCoroutine(Go());

    private IEnumerator Go()
    {
        initialAction.Invoke();
        yield return new WaitForSeconds(delay);
        navigator.GoToScene2();
    }
}
