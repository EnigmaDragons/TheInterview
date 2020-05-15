using UnityEngine;

public sealed class RandomElevatorTravel : MonoBehaviour
{
    [SerializeField] private Elevator elevator;
    [SerializeField] private int startingFloor;
    [SerializeField] private int minDestinationFloor;
    [SerializeField] private int maxDestinationFloor;

    private int _lastDestination;
    
    private void Awake()
    {
        _lastDestination = startingFloor;
        elevator.SetCurrentFloor(startingFloor);
        elevator.SetOnArrivalAction(SetNewDestination);
        SetNewDestination();
    }

    private void SetNewDestination()
    {
        var newDestination = _lastDestination;
        while (newDestination == _lastDestination)
            newDestination = Rng.Int(minDestinationFloor, maxDestinationFloor);
        elevator.SetDestination(newDestination);
        _lastDestination = newDestination;
    }
}
