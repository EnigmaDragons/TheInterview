using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public sealed class DisableCharacterMovementDuringResume : OnMessage<ResumeActivated, ResumeDeactivated>
{
    [SerializeField] private FirstPersonController controller;

    protected override void Execute(ResumeActivated msg) => controller.SetState(false);
    protected override void Execute(ResumeDeactivated msg) => controller.SetState(true);
}
