using System;
using TMPro;
using UnityEngine;

public class KeypadEntryProgram : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private TextMeshProUGUI promptLabel;
    [SerializeField] private TextMeshProUGUI codeLabel;
    [SerializeField] private TextCommandButton[] buttons;

    private string _secret;
    private string _code;
    
    private void Awake()
    {
        codeLabel.text = "";
        for(var i = 1; i < buttons.Length; i++)
        {
            var number = i % 10;
            buttons[i].Init(number.ToString(), () => EnterDigit(number));
        }
    }

    private void OnEnable()
    {
        promptLabel.text = "";
        codeLabel.text = "";
    }

    private void EnterDigit(int number)
    {
        _code = _code + number;
        // TODO: Implement this next.
    }
}
