using TMPro;
using UnityEngine;

public class Server : MonoBehaviour
{
    [SerializeField] private TextMeshPro label;

    public void Init(string group, int number) 
        => label.text = $"{@group}{number}";

    public void InvertLabel()
    {
        var scale = transform.localScale;
        transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }
}
