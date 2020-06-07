using UnityEngine;

public class PlayEndingHandler : OnMessage<PlayEnding>
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private InfiltratorNavigator navigator;
    
    protected override void Execute(PlayEnding msg)
    {
        game.UpdateState(gs => gs.CurrentRunEnding = msg.Ending);
        navigator.GoToEnding();
    }
}
