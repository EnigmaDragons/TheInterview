using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;

[Serializable]
public sealed class GameState
{
    public bool HudIsFocused = false;
    public bool HudIsLocked = true;
    public Ending CurrentRunEnding;
    public AppState AppState = new AppState();
    public bool ShouldBeHired = true;
    public PermanentCountersDictionary PermanentCounters = new PermanentCountersDictionary();
    public TransientCountersDictionary TransientCounters = new TransientCountersDictionary();
    public HashSet<string> PermanentTriggers = new HashSet<string>();
    public HashSet<string> TransientTriggers = new HashSet<string>();
    public HashSet<string> InventoryItems = new HashSet<string>();
    public Maybe<ObjectiveState> Objective;
    
    public GameState SoftReset() => 
        new GameState 
        {
            ShouldBeHired = true, 
            PermanentCounters = PermanentCounters, 
            PermanentTriggers = PermanentTriggers,
            Objective = Maybe<ObjectiveState>.Missing()
        };
    
    [Serializable] public sealed class PermanentCountersDictionary : SerializableDictionaryBase<string, int> { }
    [Serializable] public sealed class TransientCountersDictionary : SerializableDictionaryBase<string, int> { }
}
