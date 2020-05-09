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

    public void Trigger()
    {
        if (!isRecurring && _hasTriggeredThisRun)
            return;
        if (_actions == null)
            _actions = indexedUnityActions.ToDictionary(x => x.Index, x => x.Action);

        _hasTriggeredThisRun = true;
        timesTriggeredEver.Value++;
        defaultAction.Invoke();
        if (_actions.TryGetValue(timesTriggeredEver.Value, out var action))
            action.Invoke();
    }
}