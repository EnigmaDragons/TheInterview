using UnityEngine;

public class HudController : OnMessage<GameStateChanged>
{
    [SerializeField] private GameObject target;

    protected override void Execute(GameStateChanged msg) => target.SetActive(msg.State.HudIsFocused);
}
