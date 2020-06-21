using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class DigitalPackageApp : HudAppView
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Button storeButton;
    [SerializeField] private TextMeshProUGUI label;

    private Item _item;

    private void Awake() => storeButton.onClick.AddListener(Store);

    public void Init(Item item)
    {
        _item = item;
        itemImage.sprite = item.Image;
        label.text = $"You gained {item.Article} {item.Name}";
    }

    private void Store()
    {
        Message.Publish(new GainItem(_item));
        game.UnlockAndDismissHud();
    }
}
