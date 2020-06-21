using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Analytics;
using System.Collections.Generic;

[CreateAssetMenu]
public class Speech : ScriptableObject
{
    [SerializeField] private NarratorSoundPlayer narrator;
    [SerializeField] private NarratedBy voiceActor;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float volume = 0.5f;
    [SerializeField, TextArea] private string subtitle;
    [SerializeField] private UnityEvent onStarted;
    [SerializeField] private UnityEvent onFinished;
    [SerializeField] private float secondsDelay;
    [SerializeField] private SpeechPriority priority;

    public NarratedBy NarratedBy => voiceActor;
    public string Subtitle => subtitle;

    public bool CanPlay => narrator.CanPlay(priority);
    
    public IEnumerator AsyncPlay()
    {
        if (!CanPlay)
            yield break;
        
        onStarted.Invoke();
        narrator.Play(clip, priority, volume);
        PostAnalyticsEvent();
        yield return new WaitForSeconds(clip.length);
        yield return new WaitForSeconds(secondsDelay);
        onFinished.Invoke();
    }

    public void Play() => Message.Publish(new PlaySpeech { Speech = this });

    private void PostAnalyticsEvent()
    {
#if !UNITY_EDITOR
        AnalyticsEvent.Custom("speech_played", new Dictionary<string, object> {{ "speech_name", name }});
#endif
    }
}
