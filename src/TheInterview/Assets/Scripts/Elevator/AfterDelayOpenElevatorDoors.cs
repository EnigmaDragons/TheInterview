
using System.Collections;
using UnityEngine;

public class AfterDelayOpenElevatorDoors : MonoBehaviour
{
    [SerializeField] private Elevator elevator;
    [SerializeField] private FloatReference duration;

    void Awake() => StartCoroutine(OpenElevatorDoorsAfterDelay());

    private IEnumerator OpenElevatorDoorsAfterDelay()
    {
        yield return new WaitForSeconds(duration);
        elevator.OpenPhysicalDoorsImmediately();
    }
}
