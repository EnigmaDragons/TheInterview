using UnityEngine;

public class ServerGroup : MonoBehaviour
{
    [SerializeField] private Server[] servers;
    [SerializeField] private GameObject optionalAttachable;

    public int LabelServers(string column, int offset)
    {
        var invert = transform.localScale.z < 0;
        for (var i = 0; i < servers.Length; i++)
        {
            var ordinal = servers.Length - 1 - i;
            var s = servers[i];
            s.Init(column, ordinal + offset);
            if (invert)
                s.InvertLabel();
            if (optionalAttachable != null)
                Instantiate(optionalAttachable, s.transform);
        }

        return servers.Length;
    }
}
