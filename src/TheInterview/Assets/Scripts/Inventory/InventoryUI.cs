using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : OnMessage<GainItem, RemoveItem>
{
    [SerializeField] private CurrentGameState game;
    [SerializeField] private ItemIcon iconTemplate;
    [SerializeField] private List<Item> allItems;

    private readonly Dictionary<Item, ItemIcon> _items = new Dictionary<Item,ItemIcon>();

    private void Awake()
    {
        game.ReadOnly.InventoryItems.ForEach(itemName =>
        {
            var matchingItem = allItems.FirstOrDefault(item => itemName.Equals(item.Name));
            if (matchingItem == null)
                Debug.LogError($"InventoryUI doesn't have a reference to Item {itemName}");
            else
                InitItem(matchingItem);
        });
    }
    
    protected override void Execute(GainItem msg)
    {
        if (_items.TryGetValue(msg.Item, out var cachedIcon))
            cachedIcon.gameObject.SetActive(true);
        else
            InitItem(msg.Item);
    }

    private void InitItem(Item item)
    {
        var icon = Instantiate(iconTemplate, transform);
        icon.Init(item);
        _items[item] = icon;
    }

    protected override void Execute(RemoveItem msg)
    {
        if (_items.TryGetValue(msg.Item, out var cachedIcon))
            cachedIcon.gameObject.SetActive(false);
    }
}
