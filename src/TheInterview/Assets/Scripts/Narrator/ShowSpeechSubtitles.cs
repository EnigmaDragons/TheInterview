using System;
using System.Collections;
using TMPro;
using UnityEngine;

public sealed class ShowSpeechSubtitles : OnMessage<PlaySpeech>
{
    [SerializeField] private GameObject ui;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private FloatReference secondsPerCharacter;
    [SerializeField] private FloatReference delayBeforeHide;
    [SerializeField] private NarratorSoundPlayer narrator;

    private string _text = "";
    private float _t;
    private Action _onFinished;
    
    private bool IsRevealed => _t >= _text.Length * secondsPerCharacter;
    private void Reveal() => _t = _text.Length * secondsPerCharacter;
    
    private void Awake()
    {
        textBox.SetText("");
        ui.SetActive(false);
    }

    protected override void Execute(PlaySpeech msg)
    {
        var subtitle = msg.Speech.Subtitle;
        if (string.IsNullOrWhiteSpace(subtitle))
            Debug.Log($"Speech {msg.Speech.name} is missing its subtitle text.");
        else
            Display(subtitle, () => StartCoroutine(HideAfterDelay()));
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeHide);
        while (IsRevealed)
        {
            if (!narrator.IsPlaying)
            {
                ui.SetActive(false);
                yield break;
            }

            yield return new WaitForSeconds(0.33f);
        }
    }
    
    private void Display(string text, Action onFinished)
    {
        textBox.text = "";
        ui.SetActive(true);
        _onFinished = onFinished;
        _text = text;
        _t = 0;
    }

    private void Update()
    {
        if (IsRevealed)
            return;
        
        _t += Time.deltaTime;
        textBox.text = _text.Substring(0, (int) Math.Min(_text.Length, Math.Floor(_t / secondsPerCharacter)));
        if (IsRevealed)
            _onFinished();
    }
}
