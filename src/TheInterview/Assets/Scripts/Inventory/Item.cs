
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public Sprite Image;
    public UnityEvent ActivationAction;
    public string Article = "a";
}
