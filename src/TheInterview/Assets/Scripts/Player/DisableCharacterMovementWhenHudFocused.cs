using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DisableCharacterMovementWhenHudFocused : OnMessage<GameStateChanged>
{
    [SerializeField] private FirstPersonController controller;
    
    protected override void Execute(GameStateChanged msg) => controller.SetState(!msg.State.HudIsFocused);
}
