using System;
using UnityEngine;

[CreateAssetMenu(menuName = "OnlyOnce/CurrentGameOptions")]
public class CurrentGameOptions : ScriptableObject
{
    [SerializeField] private GameOptions options;

    public GameOptions Options => options;
    
    public void Init(GameOptions o) => options = o;

    public void SetShowSubtitles(bool shouldShow) => UpdateOptions(o => o.ShowSubtitles = shouldShow);

    public void UpdateOptions(Action<GameOptions> update)
    {
        UpdateOptions(o =>
        {
            update(o);
            return o;
        });
    }

    public void UpdateOptions(Func<GameOptions, GameOptions> transform)
    {
        options = transform(options);
        Message.Publish(new GameOptionsChanged(options));
    }
}
