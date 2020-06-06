using UnityEngine;
using UnityEngine.Events;

public class AutoProgressBar : MonoBehaviour
{
    [SerializeField] private ComplexProgerssBar bar;
    [SerializeField] private float totalDuration = 2f;
    [SerializeField] private UnityEvent onFinished;

    private float _elapsed;
    private bool _isFinished;
    
    private void Awake() => bar.Fraction = 0;

    private void Update()
    {
        if (_isFinished)
            return;
        
        _elapsed += Time.deltaTime;
        bar.Fraction = Mathf.Lerp(0, 1, _elapsed / totalDuration);
        _isFinished = _elapsed >= totalDuration;
        if (_isFinished)
            onFinished.Invoke();
    }
}
