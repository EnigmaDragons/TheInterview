using UnityEngine;

[CreateAssetMenu]
public class Email : ScriptableObject
{
    public string Sender;
    public string Recipient;
    public string Subject;
    [TextArea(4, 10)] public string Body;
}
