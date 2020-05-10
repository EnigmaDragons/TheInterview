using E7.Introloop;
using UnityEngine;

public class IntroLoopSceneFadeOutMusic : MonoBehaviour
{
    [SerializeField] private float fadeoutDuration = 3f;

    void OnEnable() => Execute();
    void Start() => Execute();

    void Execute()
    {
        Debug.Log("IntroLoop fadeout");
        IntroloopPlayer.Instance.Stop(fadeoutDuration);
    }
}
