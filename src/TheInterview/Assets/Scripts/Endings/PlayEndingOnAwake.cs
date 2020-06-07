using UnityEngine;

public class PlayEndingOnAwake : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;

    private void Awake()
    {
        var ending = gameState.CurrentEnding;
        Instantiate(ending.Prefab);
        ending.Speech.Play();
    }
}
