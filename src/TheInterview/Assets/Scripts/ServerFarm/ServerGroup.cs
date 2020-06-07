using UnityEngine;

public class ServerGroup : MonoBehaviour
{
    [SerializeField] private Server[] servers;

    public int LabelServers(string column, int offset)
    {
        var invert = transform.localScale.z < 0;
        for (var i = 0; i < servers.Length; i++)
        {
            var ordinal = servers.Length - 1 - i;
            servers[i].Init(column, ordinal + offset);
            if (invert)
                servers[i].InvertLabel();
        }

        return servers.Length;
    }
}
