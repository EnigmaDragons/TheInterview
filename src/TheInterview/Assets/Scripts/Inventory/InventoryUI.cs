
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : OnMessage<GainItem, RemoveItem>
{
    [SerializeField] private ItemIcon iconTemplate;

    private readonly Dictionary<Item, ItemIcon> _items = new Dictionary<Item,ItemIcon>();
    
    protected override void Execute(GainItem msg)
    {
        if (_items.TryGetValue(msg.Item, out var cachedIcon))
            cachedIcon.gameObject.SetActive(true);
        else
        {
            var icon = Instantiate(iconTemplate, transform);
            icon.Init(msg.Item);
            _items[msg.Item] = icon;
        }
    }

    protected override void Execute(RemoveItem msg)
    {
        if (_items.TryGetValue(msg.Item, out var cachedIcon))
            cachedIcon.gameObject.SetActive(false);
    }
}
