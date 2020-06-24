using UnityEngine;

public class PersistGameOptions : OnMessage<GameOptionsChanged>
{
    [SerializeField] private CurrentGameOptions options;

    private static readonly string ShowSubtitlesKey = "HideSubtitles";
    
    public void Awake() 
        => options.Init(new GameOptions { ShowSubtitles = PlayerPrefs.GetInt(ShowSubtitlesKey) == 1});

    protected override void Execute(GameOptionsChanged msg) 
        => PlayerPrefs.SetInt(ShowSubtitlesKey, msg.Options.ShowSubtitles ? 1 : 0);
}
