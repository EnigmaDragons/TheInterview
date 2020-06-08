using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerUI : OnMessage<StopTimer>
{
    [SerializeField] private TextMeshProUGUI display;

    private float _remaining;
    private Action _onTimeUp;
    
    public void StartTimer(int numSeconds, Action onTimeUp)
    {
        _onTimeUp = onTimeUp;
        _remaining = numSeconds;
        gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (_remaining <= 0)
            return;

        _remaining -= Time.deltaTime;
        display.text = _remaining.ToString("F3");

        if (!(_remaining <= 0)) 
            return;
        display.text = "0.000";
        _onTimeUp();
        StartCoroutine(ExecuteAfterDelay(2f, () => gameObject.SetActive(false)));
    }

    private IEnumerator ExecuteAfterDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    protected override void Execute(StopTimer msg)
    {
        _remaining = 0;
        StartCoroutine(ExecuteAfterDelay(2f, () => gameObject.SetActive(false)));
    }
}
