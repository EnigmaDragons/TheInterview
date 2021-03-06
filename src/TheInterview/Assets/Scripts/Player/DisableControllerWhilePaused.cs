using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public sealed class DisableControllerWhilePaused : OnMessage<GamePaused, GameContinued>
{
    [SerializeField] private FirstPersonController controller;
    [SerializeField] private CurrentGameState game;

    protected override void Execute(GamePaused msg) => controller.SetState(false);
    protected override void Execute(GameContinued msg) => controller.SetState(!game.ReadOnly.HudIsFocused);
}
