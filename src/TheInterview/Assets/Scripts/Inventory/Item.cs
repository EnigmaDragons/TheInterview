
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite Icon;
    public Sprite Image;
    public UnityEvent ActivationAction;
    public string Article = "a";
    
    public string Name => name;
}
