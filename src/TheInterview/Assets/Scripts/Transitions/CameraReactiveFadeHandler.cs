using BeautifulTransitions.Scripts.Transitions.Components.Camera;
using UnityEngine;

public sealed class CameraReactiveFadeHandler : OnMessage<StartFadeIn, StartFadeOut>
{
    [SerializeField] private FadeCamera fadeCamera;

    protected override void Execute(StartFadeIn msg)
    {
        fadeCamera.TransitionIn();
    }

    protected override void Execute(StartFadeOut msg)
    {
        fadeCamera.TransitionOut();
    }
}
