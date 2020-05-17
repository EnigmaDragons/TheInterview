using UnityEngine;

public sealed class SetPlayerZone : MonoBehaviour
{
    [SerializeField] private string zoneName;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
            Message.Publish(new PlayerIsInZone { ZoneName = zoneName });
    }
}
