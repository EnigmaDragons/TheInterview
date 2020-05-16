using UnityEngine;

public sealed class OnInterviewScheduledEnable : OnMessage<InterviewScheduled>
{
    [SerializeField] private GameObject target;

    private void Awake() => target.SetActive(false);
    protected override void Execute(InterviewScheduled msg) => target.SetActive(true);
}
