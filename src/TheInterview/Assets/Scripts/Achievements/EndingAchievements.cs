using System;
using UnityEngine;

public class EndingAchievements : CrossSceneSingleInstance
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private Achievement halfEndingsAchievement;
    [SerializeField] private Achievement allEndingsAchievement;
    [SerializeField] private AllEndings allEndings;

    private void OnEnable() => Message.Subscribe<GameStateChanged>(Execute, this);

    private void OnDisable() => Message.Unsubscribe(this);

    private void Execute(GameStateChanged msg)
    {
        if (gameState.ReadOnly.AchievedAchievements.Count == (int) Math.Ceiling(allEndings.Count / 2m))
            gameState.GainAchievement(halfEndingsAchievement);
        if (gameState.ReadOnly.AchievedAchievements.Count == allEndings.Count)
            gameState.GainAchievement(allEndingsAchievement);
    }

    protected override string UniqueTag => "Achievements";
    protected override void OnAwake() {}
}