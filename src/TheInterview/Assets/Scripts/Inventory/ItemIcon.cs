using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    public Item Item;
    
    public void Init(Item item)
    {
        Item = item;
        icon.sprite = item.Icon;
        button.onClick.AddListener(() => item.ActivationAction.Invoke());
    }
}
