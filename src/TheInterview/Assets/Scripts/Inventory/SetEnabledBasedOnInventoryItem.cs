
using UnityEngine;

public class SetEnabledBasedOnInventoryItem : OnMessage<GameStateChanged>
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private GameObject target;
    [SerializeField] private Item item;
    [SerializeField] private bool shouldBeEnabledIfHasItem = true;

    private void Awake() => UpdateState(game.ReadOnly);
    protected override void Execute(GameStateChanged msg) => UpdateState(msg.State);

    private void UpdateState(GameState state)
    {
        var hasItem = state.InventoryItems.Contains(item.Name);
        target.SetActive(shouldBeEnabledIfHasItem ? hasItem : !hasItem); 
    }
}
