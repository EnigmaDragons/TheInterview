
using UnityEngine;

public class SetStateBasedOnSubtitlesEnabled : OnMessage<GameOptionsChanged>
{
    [SerializeField] private CurrentGameOptions current;
    [SerializeField] private GameObject target;

    private void Awake() => target.SetActive(current.Options.ShowSubtitles);

    protected override void Execute(GameOptionsChanged msg) => target.SetActive(msg.Options.ShowSubtitles);
}
