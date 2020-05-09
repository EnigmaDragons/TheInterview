using UnityEngine;

class InitNarratorSoundPlayer : CrossSceneSingleInstance
{
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private NarratorSoundPlayer narratorPlayer;

    protected override string UniqueTag => "NarratorSounds";
    protected override void OnAwake() => narratorPlayer.Init(uiAudioSource);
}
