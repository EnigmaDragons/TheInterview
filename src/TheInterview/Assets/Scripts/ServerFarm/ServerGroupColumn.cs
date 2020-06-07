using UnityEngine;

public class ServerGroupColumn : MonoBehaviour
{
    [SerializeField] private ServerGroup[] groups;

    public void LabelServers(string column)
    {
        var serverNumber = 1;
        foreach (var group in groups) 
            serverNumber += group.LabelServers(column, serverNumber);
    }
}
