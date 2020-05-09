using UnityEngine;

public class SpeechPlayer : OnMessage<PlaySpeech>
{
    protected override void Execute(PlaySpeech msg)
    {
        Debug.Log($"Playing Speech {msg.Speech.name}");
        StartCoroutine(msg.Speech.AsyncPlay());
    }
}