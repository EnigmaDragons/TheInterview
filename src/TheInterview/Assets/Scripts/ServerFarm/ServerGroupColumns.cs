using UnityEngine;

public class ServerGroupColumns : MonoBehaviour
{
    private static readonly string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    [SerializeField] private ServerGroupColumn[] groups;

    private void Awake()
    {
        for (var i = 0; i < groups.Length; i++)
            groups[i].LabelServers(Alphabet[i].ToString());
    }
}
