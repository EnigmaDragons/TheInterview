using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YesNoSurveyApp : MonoBehaviour
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private TextMeshProUGUI prompt;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private AudioClipVolume yesSound;
    [SerializeField] private AudioClipVolume noSound;
    [SerializeField] private UiSfxPlayer sfx;

    private YesNoSurveyQuestion _question;

    private void Awake()
    {
        prompt.text = "";
        yesButton.onClick.AddListener(Yes);
        noButton.onClick.AddListener(No);
    }
    
    public void Init(YesNoSurveyQuestion q)
    {
        prompt.text = q.Prompt;
        _question = q;
    }

    private void Yes()
    {
        if (_question == null)
            return;
        
        _question.OnYes.Invoke();
        sfx.Play(yesSound);
        game.UnlockAndDismissHud();
    }
    
    private void No()
    {
        if (_question == null)
            return;
        
        _question.OnNo.Invoke();
        sfx.Play(noSound);
        game.UnlockAndDismissHud();
    }
}
