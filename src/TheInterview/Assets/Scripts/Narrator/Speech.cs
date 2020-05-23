﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Speech : ScriptableObject
{
    [SerializeField] private NarratorSoundPlayer narrator;
    [SerializeField] private NarratedBy voiceActor;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float volume = 0.5f;
    [SerializeField, TextArea] private string subtitle;
    [SerializeField] private UnityEvent onFinished;
    [SerializeField] private float secondsDelay;
    [SerializeField] private bool shouldInterupt = true;

    public NarratedBy NarratedBy => voiceActor;
    public string Subtitle => subtitle;
    
    public IEnumerator AsyncPlay()
    {
        if (shouldInterupt || !narrator.IsPlaying)
        {
            narrator.Play(clip, volume);
            yield return new WaitForSeconds(clip.length);
            yield return new WaitForSeconds(secondsDelay);
            onFinished.Invoke();
        }
    }

    public void Play() => Message.Publish(new PlaySpeech { Speech = this });
}
