using UnityEngine;

public class OnEnableLockDoor : MonoBehaviour
{
    [SerializeField] private Door door;

    private void OnEnable() => door.SetCanBeOpened(false);
}
