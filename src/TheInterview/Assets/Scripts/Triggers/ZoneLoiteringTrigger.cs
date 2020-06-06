using System.Collections;
using UnityEngine;

public sealed class ZoneLoiteringTrigger : OnMessage<PlayerIsInZone, PlaySpeech>
{
    [SerializeField] private string zoneName;
    [SerializeField] private TriggerData triggerData;
    [SerializeField] private FloatReference loiterTime;

    private bool _isInZone;
    private float _currentLoiterTime;

    private void Awake() => StartCoroutine(Execute());

    private IEnumerator Execute()
    {
        while (true)
        {
            var timeClick = 0.2f;
            yield return new WaitForSeconds(timeClick);
            if(_isInZone)
                _currentLoiterTime += timeClick;

            if (_currentLoiterTime >= loiterTime) 
                ActivateLoiterTrigger();
        }
    }

    private void ActivateLoiterTrigger()
    {
        _currentLoiterTime = 0;
        triggerData.Trigger();
    }

    protected override void Execute(PlayerIsInZone msg)
    {
        _isInZone = msg.ZoneName.Equals(zoneName);
        _currentLoiterTime = 0;
    }

    protected override void Execute(PlaySpeech msg) => _currentLoiterTime = 0;
}
