
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite Icon;
    public UnityEvent ActivationAction;
    
    public string Name => name;
}
