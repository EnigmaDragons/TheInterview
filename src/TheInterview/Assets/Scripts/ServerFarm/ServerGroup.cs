using UnityEngine;

public class ServerGroup : MonoBehaviour
{
    [SerializeField] private Server[] servers;

    public int LabelServers(string column, int offset)
    {
        var invertLabel = transform.localScale.z < 0;
        for (var i = 0; i < servers.Length; i++)
        {
            servers[i].Init(column, i + offset);
            if (invertLabel)
                servers[i].InvertLabel();
        }

        return servers.Length;
    }
}
