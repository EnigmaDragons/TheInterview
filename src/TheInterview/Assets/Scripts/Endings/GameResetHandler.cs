﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameResetHandler : OnMessage<ResetGameAfterDelay>
{
    [SerializeField] private FloatReference delay;
    [SerializeField] private UnityEvent initialAction;
    [SerializeField] private UnityEvent resetAction;
    [SerializeField] private CurrentGameState game;
    
    protected override void Execute(ResetGameAfterDelay msg) => StartCoroutine(Go());

    private IEnumerator Go()
    {
        initialAction.Invoke();
        yield return new WaitForSeconds(delay);
        game.SoftReset();
        resetAction.Invoke();
    }
}
