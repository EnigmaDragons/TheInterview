using UnityEngine;
using UnityEngine.UI;

public class Button2 : MonoBehaviour
{
    [SerializeField] private Sprite blueLight;
    [SerializeField] private Sprite redLight;
    [SerializeField] private Sprite blueLineLight;
    [SerializeField] private Sprite redLineLight;
    [SerializeField] private Image light;
    [SerializeField] private Image lineLight;
    [SerializeField] private Text text;

    public Button Button;

    public void SetToBlue()
    {
        light.sprite = blueLight;
        text.color = new Color32(0x00, 0xFF, 0xFF, 0xFF);
        lineLight.sprite = blueLineLight;
    }

    public void SetToRed()
    {
        light.sprite = redLight;
        text.color = new Color32(0xD4, 0x53, 0x8E, 0xFF);
        lineLight.sprite = redLineLight;
    }
}