using TMPro;
using UnityEngine;

public class StaticFloorNumber : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;

    private void Awake() => text.text = transform.FloorNumber().ToString();
}
