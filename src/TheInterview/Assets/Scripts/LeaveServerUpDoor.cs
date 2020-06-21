using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LeaveServerUpDoor : MonoBehaviour
{
    [SerializeField] private CurrentGameState gameState;
    [SerializeField] private TriggerData hackedB3;
    [SerializeField] private TriggerData[] buildingOnFire;
    [SerializeField] private TriggerData securityTeamCalled;
    [SerializeField] private TriggerData betrayedByInfiltratorGO;
    [SerializeField] private Speech easyJob;
    [SerializeField] private Speech excitingJob;
    [SerializeField] private Speech burnedJob;
    [SerializeField] private Speech postponedJob;
    [SerializeField] private Speech betrayedJob;
    [SerializeField] private InteractAction interactable;
    [SerializeField] private UnityEvent executeEvent;

    private void OnEnable()
    {
        interactable.Add(executeEvent);
    }

    public void Execute()
    {
        var didHackB3 = gameState.HasTriggeredThisRun(hackedB3.name);
        var isBuildingOnFire = buildingOnFire.Any(x => gameState.HasTriggeredThisRun(x.name));
        var isSecurityInbound = gameState.HasTriggeredThisRun(securityTeamCalled.name);
        var isBetrayedByInfiltratorGO = gameState.HasTriggeredThisRun(betrayedByInfiltratorGO.name);
        if (didHackB3 && (isBuildingOnFire || isSecurityInbound))
            excitingJob.Play();
        else if (didHackB3)
            easyJob.Play();
        else if (isBuildingOnFire)
            burnedJob.Play();
        else if (isSecurityInbound)
            postponedJob.Play();
        else if (isBetrayedByInfiltratorGO)
            betrayedJob.Play();
    }
}