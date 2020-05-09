using System.Linq;
using UnityEngine;

public class SetDoorInteractable : MonoBehaviour
{
    [SerializeField] private int[] enabledFloors;
    [SerializeField] private Door door;

    private void Awake() 
        => door.SetCanBeOpened(enabledFloors.Contains(door.transform.FloorNumber()));
}
