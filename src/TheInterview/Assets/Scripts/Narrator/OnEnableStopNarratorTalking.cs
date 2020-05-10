
using UnityEngine;

public class OnEnableStopNarratorTalking : MonoBehaviour
{
    [SerializeField] private NarratorSoundPlayer narrator;

    void OnEnable() => narrator.Stop();
}
