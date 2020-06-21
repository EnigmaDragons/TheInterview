
using UnityEngine;

public class GainItemHandler : OnMessage<GainItem, RemoveItem>
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private UiSfxPlayer sfx;
    [SerializeField] private AudioClipVolume gainItemSound;

    protected override void Execute(GainItem msg)
    {
        sfx.Play(gainItemSound);
        game.UpdateState(gs => gs.InventoryItems.Add(msg.Item.Name));
    }

    protected override void Execute(RemoveItem msg)
        => game.UpdateState(gs => gs.InventoryItems.Remove(msg.ItemName));
}
