using System.Linq;
using UnityEngine;

public class LeaveServerUpDoor : OnMessage<GameStateChanged>
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
    [SerializeField] private GameObject targetToEnable;

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

    protected override void Execute(GameStateChanged msg)
    {
        if (gameState.HasTriggeredThisRun(hackedB3.name) 
            || buildingOnFire.Any(x => gameState.HasTriggeredThisRun(x.name)) 
            || gameState.HasTriggeredThisRun(securityTeamCalled.name) 
            || gameState.HasTriggeredThisRun(betrayedByInfiltratorGO.name))
            targetToEnable.SetActive(true);
    }
}