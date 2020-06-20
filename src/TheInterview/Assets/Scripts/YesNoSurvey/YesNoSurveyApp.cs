
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YesNoSurveyApp : MonoBehaviour
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private TextMeshProUGUI prompt;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    public void Init(YesNoSurveyQuestion q)
    {
        prompt.text = q.Prompt;
        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(() => q.OnYes.Invoke());
        yesButton.onClick.AddListener(() => game.UnlockAndDismissHud());
        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(() => q.OnNo.Invoke());
        noButton.onClick.AddListener(() => game.UnlockAndDismissHud());
    }
}
