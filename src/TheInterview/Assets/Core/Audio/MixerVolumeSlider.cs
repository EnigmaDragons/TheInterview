using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public sealed class MixerVolumeSlider : MonoBehaviour 
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;
    [SerializeField] private string valueName = "MusicVolume";
    [SerializeField] private FloatReference reductionDb = new FloatReference(0f);

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(valueName, 0.5f);
        slider.onValueChanged.AddListener(SetLevel);
    }
    
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(valueName, (Mathf.Log10(sliderValue) * 20) - reductionDb);
        PlayerPrefs.SetFloat(valueName, sliderValue);
        Message.Publish(new MixerVolumeChanged(valueName));
    }
}
