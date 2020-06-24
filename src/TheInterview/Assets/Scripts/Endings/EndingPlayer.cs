using System.Linq;
using UnityEngine;

public class EndingPlayer : OnMessage<EndingSpeechFinished>
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private InfiltratorNavigator navigator;

    private void Awake()
    {
        var ending = gameState.CurrentEnding;
        Instantiate(ending.Prefab);
        ending.Speech.Play();
    }

    protected override void Execute(EndingSpeechFinished msg)
    {
        var achievementToPlay = gameState.ReadOnly.AchievedAchievements.FirstOrDefault(x => !x.HasPlayedSpeech);
        if (achievementToPlay == null)
        {
            gameState.SoftReset();
            navigator.GoToStartingApartment();
        }
        else
        {
            achievementToPlay.Achievement.Speech.Play();
            achievementToPlay.HasPlayedSpeech = true;
        }
    }
}
