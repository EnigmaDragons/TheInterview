using UnityEngine;

public class PersistGameOptions : OnMessage<GameOptionsChanged>
{
    [SerializeField] private CurrentGameOptions options;

    private static readonly string HideSubtitlesKey = "HideSubtitles";
    
    public void Awake() 
        => options.Init(new GameOptions { ShowSubtitles = PlayerPrefs.GetInt(HideSubtitlesKey) == 0});

    protected override void Execute(GameOptionsChanged msg) 
        => PlayerPrefs.SetInt(HideSubtitlesKey, msg.Options.ShowSubtitles ? 0 : 1);
}
