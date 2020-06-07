using TMPro;
using UnityEngine;

public class KeypadEntryProgram : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private TextMeshProUGUI promptLabel;
    [SerializeField] private TextMeshProUGUI codeLabel;
    [SerializeField] private TextCommandButtonNonTMP[] buttons;

    private string _secret;
    private string _code;
    
    private void Awake()
    {
        codeLabel.text = "";
        for(var i = 0; i < buttons.Length; i++)
        {
            var number = (i + 1) % 10;
            buttons[i].Init(number.ToString(), () => EnterDigit(number));
        }
    }

    public void Init(CodeHackSecret secret)
    {
        _code = "";
        promptLabel.text = secret.DeviceName.Value;
        _secret = secret.SecretCode;
    }

    private void EnterDigit(int number)
    {
        _code = _code + number;
        // TODO: Implement this next.
    }
}
