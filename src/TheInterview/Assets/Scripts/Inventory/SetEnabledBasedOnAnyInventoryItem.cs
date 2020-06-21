using System.Linq;
using UnityEngine;

public class SetEnabledBasedOnAnyInventoryItem : OnMessage<GameStateChanged>
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private GameObject target;
    [SerializeField] private Item[] items;
    [SerializeField] private bool shouldBeEnabledIfHasItem = true;

    private void Awake() => UpdateState(game.ReadOnly);
    protected override void Execute(GameStateChanged msg) => UpdateState(msg.State);

    private void UpdateState(GameState state)
    {
        var hasAnyItem = state.InventoryItems.Any(x => items.Any(i => string.Equals(i.Name, x)));
        target.SetActive(shouldBeEnabledIfHasItem ? hasAnyItem : !hasAnyItem); 
    }
}

