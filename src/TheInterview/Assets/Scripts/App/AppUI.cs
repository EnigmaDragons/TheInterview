using System;
using UnityEngine;

public class AppUI : OnMessage<AppStateChanged>
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private GameObject appParent;

    private void OnEnable()
    {
        appParent.SetActive(gameState.AppState.AppInstalled);
    }

    protected override void Execute(AppStateChanged msg)
    {
        appParent.SetActive(gameState.AppState.AppInstalled);
    }
}