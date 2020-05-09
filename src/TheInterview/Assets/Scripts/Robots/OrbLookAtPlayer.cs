
using UnityEngine;

public class OrbLookAtPlayer : MonoBehaviour
{
    private GameObject player;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        transform.LookAt(player.transform);
    }
}
