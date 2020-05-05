using E7.Introloop;
using UnityEngine;

public class IntroLoopSceneBackgroundMusic : MonoBehaviour
{
    [SerializeField] private IntroloopAudio music;
    [SerializeField] private IntroLoopAudioPlayer musicPlayer;

    private void Start()
    {
        musicPlayer.PlaySelectedMusicLooping(music);
    }
}
