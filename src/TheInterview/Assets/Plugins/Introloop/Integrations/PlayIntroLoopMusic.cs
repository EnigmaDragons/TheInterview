using E7.Introloop;
using UnityEngine;

public sealed class PlayIntroLoopMusic : MonoBehaviour
{
    [SerializeField] private IntroloopAudio music;
    [SerializeField] private IntroLoopAudioPlayer player;

    public void Execute() => player.PlaySelectedMusicLooping(music);
}
