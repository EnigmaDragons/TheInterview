using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class AdminStatusScreenText : MonoBehaviour
{
    [SerializeField] private TextMeshPro display;
    [SerializeField] private string client = "Confidential";
    [SerializeField] private string column = "A";
    [SerializeField] private int startingNumber = 1;
    [SerializeField] private int maxNumber = 40;
    [SerializeField] private List<IndexedName> clientOverrides;

    void Awake()
    {
        var sb = new StringBuilder();
        for (var i = startingNumber; i <= maxNumber; i++)
            sb.AppendLine(ServerStatusString($"{ServerNumber(i)}", Client(i)));
        display.text = sb.ToString();
    }

    private string ServerNumber(int index) => column + (index < 10 ? "0" + index : index.ToString());

    private string ServerStatusString(string server, string serverClient) => $"Server: {server.PadLeft(3)} | Status: Online | Uptime: {Uptime().PadLeft(5)} | Client: {serverClient.PadRight(20)}";
    private string Uptime() => (Rng.Dbl() - 0.01 + 99).ToString("F4") + "%";

    private string Client(int index)
    {
        var customClient = clientOverrides.FirstOrDefault(x => x.Index == index);
        return customClient != null ? customClient.Name : client;
    }
}
