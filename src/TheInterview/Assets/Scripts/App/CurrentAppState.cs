﻿using System;
using UnityEngine;

[CreateAssetMenu(menuName = "OnlyOnce/CurrentAppState")]
public class CurrentAppState : ScriptableObject
{
    [SerializeField] private CurrentGameState gameState;

    public AppState State => gameState.AppState;
    
    public void InstallApp() => UpdateState(app => app.AppInstalled = true);
    public void AddCredits(int amount) => UpdateState(app => app.Creds += amount);
    
    public void UpdateState(Action<AppState> apply)
    {
        UpdateState(appState =>
        {
            apply(appState);
            return appState;
        });
    }
    
    public void UpdateState(Func<AppState, AppState> apply)
    {
        gameState.UpdateState(gs => gs.AppState = apply(gs.AppState));
        Message.Publish(new AppStateChanged(gameState.AppState));
    }
}
