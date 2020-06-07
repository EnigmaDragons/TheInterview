
using UnityEngine;

public class GameTriggerDoorLock : OnMessage<GameStateChanged>
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private Door door;
    [SerializeField] private StringVariable deviceId;

    private void Awake() => UpdateDoor(game.ReadOnly);
    protected override void Execute(GameStateChanged msg) => UpdateDoor(msg.State);
    
    private void UpdateDoor(GameState g) => door.SetCanBeOpened(g.IsTriggered(deviceId.Value));
}
