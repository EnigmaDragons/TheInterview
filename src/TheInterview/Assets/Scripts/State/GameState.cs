using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;

[Serializable]
public sealed class GameState
{
    public bool ShouldBeHired = true;
    public PermanentCountersDictionary PermanentCounters = new PermanentCountersDictionary();
    public TransientCountersDictionary TransientCounters = new TransientCountersDictionary();
    public HashSet<string> TransientTriggers = new HashSet<string>();
    
    public GameState SoftReset() => 
        new GameState 
        {
            ShouldBeHired = true, 
            PermanentCounters = PermanentCounters
        };

    [Serializable] public sealed class PermanentCountersDictionary : SerializableDictionaryBase<string, int> { }
    [Serializable] public sealed class TransientCountersDictionary : SerializableDictionaryBase<string, int> { }
}
