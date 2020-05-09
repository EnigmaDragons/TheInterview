using System.Collections;
using UnityEngine;

public class GameResetHandler : OnMessage<ResetGameAfterDelay>
{
    [SerializeField] private FloatReference delay;
    [SerializeField] private Navigator navigator;
    
    protected override void Execute(ResetGameAfterDelay msg) => StartCoroutine(Go());

    private IEnumerator Go()
    {
        yield return new WaitForSeconds(delay);
        navigator.GoToScene2();
    }
}
