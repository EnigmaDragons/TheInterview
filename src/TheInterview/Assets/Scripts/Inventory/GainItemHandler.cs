
using UnityEngine;

public class GainItemHandler : OnMessage<GainItem, RemoveItem>
{
    [SerializeField] private CurrentGameState game;

    protected override void Execute(GainItem msg) 
        => game.UpdateState(gs => gs.InventoryItems.Add(msg.Item.Name));

    protected override void Execute(RemoveItem msg)
        => game.UpdateState(gs => gs.InventoryItems.Remove(msg.ItemName));
}
