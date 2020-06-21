using System;
using UnityEngine;

[CreateAssetMenu]
public class TriggerData : ScriptableObject
{
    [SerializeField] private CurrentGameState gameState;
    public TriggerInteractType InteractType;
    public TriggerRepeatability Repeatability;
    public TriggerStateLifecycle Lifecycle;
    public bool RepeatLastLine = false;
    [SerializeField] private Speech[] speeches = new Speech[0];

    public bool CanTrigger => GetNextSpeech().IsPresentAnd(s => s.CanPlay);

    private Maybe<Speech> GetNextSpeech()
    {
        var nextIndex =  gameState.Counter(Lifecycle, name);
        if (Repeatability == TriggerRepeatability.OncePerRun && gameState.HasTriggeredThisRun(name))
            return Maybe<Speech>.Missing();
        if (speeches.Length > nextIndex)
            return speeches[nextIndex];
        if (RepeatLastLine)
            return speeches[speeches.Length - 1];
        return Maybe<Speech>.Missing();
    }
    
    public void Trigger()
    {
        var nextSpeech = GetNextSpeech();
        if (nextSpeech.IsMissing)
            throw new Exception($"Attempted to execute a Trigger with no valid speech {name}");

        if (!nextSpeech.Value.CanPlay)
            return;

        nextSpeech.Value.Play();
        gameState.IncrementCounter(Lifecycle, name);
    }
}