
using UnityEngine;

public class InitElevatorState : MonoBehaviour
{
    [SerializeField] private Elevator elevator;
    [SerializeField] private int startingFloor;
    [SerializeField] private int destinationFloor;
    [SerializeField] private bool autoStartElevator;
    
    private void Start()
    {
        elevator.SetCurrentFloor(startingFloor);
        elevator.SetDestination(destinationFloor);
        if (autoStartElevator)
            elevator.TravelTo(destinationFloor);
    }
}
