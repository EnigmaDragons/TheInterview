
using UnityEngine;

public class InitElevatorState : MonoBehaviour
{
    [SerializeField] private Elevator elevator;
    [SerializeField] private int startingFloor;
    [SerializeField] private int destinationFloor;
    
    private void Start()
    {
        elevator.SetCurrentFloor(startingFloor);
        elevator.SetDestination(destinationFloor);
    }
}
