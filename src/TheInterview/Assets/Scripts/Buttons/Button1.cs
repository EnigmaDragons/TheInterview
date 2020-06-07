using UnityEngine;
using UnityEngine.UI;

public class Button1 : MonoBehaviour
{
    [SerializeField] private Sprite blueBtn;
    [SerializeField] private Sprite redBtn;
    [SerializeField] private Sprite blueLight;
    [SerializeField] private Sprite redLight;
    [SerializeField] private Image btn;
    [SerializeField] private Image light;

    public Button Button;

    public void SetToBlue()
    {
        btn.sprite = blueBtn;
        light.sprite = blueLight;
    }

    public void SetToRed()
    {
        btn.sprite = redBtn;
        light.sprite = redLight;
    }
}