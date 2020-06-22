using TMPro;
using UnityEngine;

public class EndingProgressUI : MonoBehaviour
{
    [SerializeField] private AllEndings endings;
    [SerializeField] private CurrentGameState game;
    [SerializeField] private TextMeshProUGUI display;

    private void Awake() 
        => display.text = $"Endings Achieved:\n{game.ReadOnly.PermanentCounters["EndingsCompleted"]} / {endings.Count}";
}
