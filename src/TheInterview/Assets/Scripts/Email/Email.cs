using UnityEngine;

[CreateAssetMenu]
public class Email : ScriptableObject
{
    public string Sender;
    public string Recipient;
    public string Subject;
    [TextArea(minLines: 3, maxLines: 20)] public string Body;
}
