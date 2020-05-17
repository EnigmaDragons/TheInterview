using UnityEngine;

public sealed class OnEnablePlaySpeech : MonoBehaviour
{
    [SerializeField] private Speech speech;

    void OnEnable() => StartCoroutine(speech.AsyncPlay());
}
