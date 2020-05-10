using UnityEngine;

[CreateAssetMenu(menuName = "Only Once/Narrator Sound Player")]
public class NarratorSoundPlayer : ScriptableObject
{
    [SerializeField] private AudioSource source;

    public bool IsPlaying => source.isPlaying;

    public void Init(AudioSource src) => source = src;

    public void Play(AudioClip clipToPlay, float volume)
    {
        if (source == null)
        {
            Debug.LogError($"nameof(musicSource) has not been initialized");
            return;
        }

        if (source.isPlaying && source.clip.name.Equals(clipToPlay.name))
            return;

        StopIfPlaying();
        source.clip = clipToPlay;
        source.volume = volume;
        source.loop = false;
        source.Play();
    }

    private void StopIfPlaying()
    {
        if (source != null && source.isPlaying)
            source.Stop();
    }

    public void Stop() => StopIfPlaying();
}