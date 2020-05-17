using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class OnTrigger
{
    [SerializeField] private bool isRecurring;
    [SerializeField] private IntVariable timesTriggeredEver;
    [SerializeField] private UnityEvent defaultAction;
    [SerializeField] private IndexedUnityEvent[] indexedUnityActions;

    private bool _hasTriggeredThisRun;
    private Dictionary<int, UnityEvent> _actions;
    
    public bool CanTrigger => isRecurring || !_hasTriggeredThisRun && HasPossibleAction;
    private bool HasPossibleAction => OnlyUsesDefaultAction || HasUntriggeredIndexedAction;
    private bool OnlyUsesDefaultAction => indexedUnityActions.Length == 0;
    private bool HasUntriggeredIndexedAction => IndexedActions.ContainsKey(NextIndexedUnityEventIndex);
    private int NextIndexedUnityEventIndex => timesTriggeredEver != null ? timesTriggeredEver.Value + 1 : 0;
    private Dictionary<int, UnityEvent> IndexedActions
    {
        get
        {
            if (_actions == null)
                _actions = indexedUnityActions.ToDictionary(x => x.Index, x => x.Action);
            return _actions;
        }
    }
    
    public void Trigger()
    {
        if (!CanTrigger)
            return;

        _hasTriggeredThisRun = true;
        defaultAction.Invoke();
        if (IndexedActions.TryGetValue(NextIndexedUnityEventIndex, out var action))
            action.Invoke();
        if (timesTriggeredEver != null)
            timesTriggeredEver.Value++;
    }
}