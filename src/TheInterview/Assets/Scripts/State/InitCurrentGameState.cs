using UnityEngine;

public sealed class InitCurrentGameState : MonoBehaviour
{
    [SerializeField] private CurrentGameState state;

    void Awake() => state.Init();
}
