using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class TimerData : ScriptableObject 
{
    public int NumSeconds;
    public UnityEvent OnTimeUp;
}
