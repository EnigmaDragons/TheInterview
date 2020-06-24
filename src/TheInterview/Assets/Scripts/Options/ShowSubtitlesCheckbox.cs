
using UnityEngine;
using UnityEngine.UI;

public class ShowSubtitlesCheckbox : MonoBehaviour
{
    [SerializeField] private CurrentGameOptions current;
    [SerializeField] private Toggle toggle;
    
    private void Start()
    {
        toggle.isOn = current.Options.ShowSubtitles;
        toggle.onValueChanged.AddListener(v => current.SetShowSubtitles(v));
    }
}
