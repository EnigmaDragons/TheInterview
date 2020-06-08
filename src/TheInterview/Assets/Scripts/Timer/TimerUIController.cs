
using UnityEngine;

public class TimerUIController : OnMessage<StartTimer>
{
    [SerializeField] private TimerUI timer;
    
    protected override void Execute(StartTimer msg)
    {
        timer.StartTimer(msg.Data.NumSeconds, () => msg.Data.OnTimeUp.Invoke());
    }
}
