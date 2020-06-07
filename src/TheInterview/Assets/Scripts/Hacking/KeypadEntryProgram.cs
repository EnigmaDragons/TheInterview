using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class KeypadEntryProgram : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private TextMeshProUGUI promptLabel;
    [SerializeField] private TextMeshProUGUI codeLabel;
    [SerializeField] private TextCommandButtonNonTMP[] buttons;
    [SerializeField] private UiSfxPlayer sfx;
    [SerializeField] private AudioClip incorrectEntry;
    [SerializeField] private AudioClip correctEntry;

    private StringVariable _deviceId;
    private string _prompt;
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
        ClearEntry();
        _prompt = secret.Prompt;
        _deviceId = secret.DeviceId;
        _secret = secret.SecretCode;
    }

    public void ClearEntry()
    {
        _code = "";
        codeLabel.text = "";
        promptLabel.text = _prompt;
    }

    private bool EntryComplete => _code.Length == _secret.Length;
    
    private void EnterDigit(int number)
    {
        if (EntryComplete)
            return;
        
        _code = _code + number;
        Debug.Log($"Code - Secret is: {_secret}. Entered: {_code}");
        codeLabel.text = string.Join("", Enumerable.Range(0, _code.Length).Select(_ => "*"));
        if (EntryComplete)
            ResolveCode();
    }

    private void ResolveCode()
    {
        if (string.Equals(_code, _secret))
        {
            sfx.Play(correctEntry);
            promptLabel.text = "Access Granted";
            StartCoroutine(ExecuteAfterDelay(1f, 
                () => gameState.UpdateState(gs =>
                {
                    gs.TransientTriggers.Add(_deviceId.Value);
                    gs.HudIsFocused = false;
                })));
        }
        else
        {
            sfx.Play(incorrectEntry);
            promptLabel.text = "Access Denied";
            StartCoroutine(ExecuteAfterDelay(1f, ClearEntry));
        }
    }

    private IEnumerator ExecuteAfterDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
