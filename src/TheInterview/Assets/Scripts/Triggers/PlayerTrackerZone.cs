using UnityEngine;

public class PlayerTrackerZone : MonoBehaviour
{
    [SerializeField] private bool isPlayerIn;

    public bool IsPlayerIn => isPlayerIn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerIn = true;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerIn = false;
    }
}
