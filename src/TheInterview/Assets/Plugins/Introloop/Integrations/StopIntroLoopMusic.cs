using UnityEngine;

public sealed class StopIntroLoopMusic : MonoBehaviour
{
    [SerializeField] private IntroLoopAudioPlayer player;

    public void Execute() => player.Stop();
}
