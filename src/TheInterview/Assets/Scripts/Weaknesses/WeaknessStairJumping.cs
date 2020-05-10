using UnityEngine;

public class WeaknessStairJumping : OnMessage<WeaknessSelected>
{
    [SerializeField] private Speech weaknessSpeech;
    [SerializeField] private Speech[] preSpeeches;
    [SerializeField] private Speech[] postSpeeches;
    [SerializeField] private IntVariable stairJumpedCounter;

    private bool _preWeakness;

    private void Awake() => stairJumpedCounter.Value = 0;

    protected override void Execute(WeaknessSelected msg)
    {
        _preWeakness = false;
        stairJumpedCounter.Value = 0;
    }

    public void Play()
    {
        if (_preWeakness && preSpeeches.Length >= stairJumpedCounter.Value)
            preSpeeches[stairJumpedCounter.Value - 1].Play();
        else if (!_preWeakness && postSpeeches.Length >- stairJumpedCounter.Value)
            postSpeeches[stairJumpedCounter.Value - 1].Play();
    }
}