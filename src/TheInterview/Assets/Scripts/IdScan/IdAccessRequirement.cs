using UnityEngine;

[CreateAssetMenu]
public class IdAccessRequirement : ScriptableObject
{
    public string Location;
    public Item RequiredId;
    public StringVariable AccessTarget;
}
