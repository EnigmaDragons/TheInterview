using UnityEngine;

[CreateAssetMenu(menuName = "Only Once/Narrator Sound Player")]
public class NarratorSoundPlayer : ScriptableObject
{
    [SerializeField] private AudioSource source;
    [SerializeField] private SpeechPriority currentClipPriority;

    public bool IsPlaying => source.isPlaying;
    public bool CanPlay(SpeechPriority priority) => 
        !IsPlaying || priority == SpeechPriority.High || priority > currentClipPriority;

    public void Init(AudioSource src) => source = src;

    public void Play(AudioClip clipToPlay, SpeechPriority priority, float volume)
    {
        if (source == null)
        {
            Debug.LogError($"nameof(musicSource) has not been initialized");
            return;
        }

        if (source.isPlaying && source.clip.name.Equals(clipToPlay.name))
            return;

        currentClipPriority = priority;
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