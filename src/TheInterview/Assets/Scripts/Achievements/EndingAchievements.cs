using System;
using UnityEngine;

public class EndingAchievements : OnMessage<GameStateChanged>
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private Achievement halfEndingsAchievement;
    [SerializeField] private Achievement allEndingsAchievement;
    [SerializeField] private AllEndings allEndings;

    protected override void Execute(GameStateChanged msg)
    {
        if (gameState.ReadOnly.AchievedAchievements.Count == (int) Math.Ceiling(allEndings.Count / 2m))
            gameState.GainAchievement(halfEndingsAchievement);
        if (gameState.ReadOnly.AchievedAchievements.Count == allEndings.Count)
            gameState.GainAchievement(allEndingsAchievement);
    }
}