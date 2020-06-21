using UnityEngine;

[CreateAssetMenu]
public class Email : ScriptableObject
{
    public string Sender;
    public string Recipient;
    public string Subject;
    public string Body;
}
