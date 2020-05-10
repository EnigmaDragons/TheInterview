using System.Linq;
using UnityEngine;

public class SpeechPlayer : OnMessage<PlaySpeech>
{
    [SerializeField] private NarratedBy[] includedVoiceActors;
    
    protected override void Execute(PlaySpeech msg)
    {
        if (!includedVoiceActors.Contains(msg.Speech.NarratedBy))
        {
            Debug.LogWarning($"Not Playing Requested Speech {msg.Speech.name}, since it is narrated by {msg.Speech.NarratedBy}");
            return;
        }
        
        Debug.Log($"Playing Speech {msg.Speech.name}");
        StartCoroutine(msg.Speech.AsyncPlay());
    }
}