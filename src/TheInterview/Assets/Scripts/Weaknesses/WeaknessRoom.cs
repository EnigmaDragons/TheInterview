using UnityEngine;

public class WeaknessRoom : OnMessage<Finished<WeaknessSelected>>
{
    [SerializeField] private GameObject[] weaknessButtons;
    [SerializeField] private int targetNumber = 3;
    [SerializeField] private Door door;

    private int _count;

    protected override void Execute(Finished<WeaknessSelected> msg)
    {
        _count++;
        if (_count == targetNumber)
        {
            for (var i = 0; i < weaknessButtons.Length; i++)
                weaknessButtons[i].SetActive(false);
            door.SetCanBeOpened(true);
            door.OpenDoor();
            door.SetCanBeOpened(false);
        }
    }
}