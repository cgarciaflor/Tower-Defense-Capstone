using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public enum VolumeType { Master, Music, SFX }
    public VolumeType volumeType;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        // Load saved value
        float defaultValue = 1f;
        switch (volumeType)
        {
            case VolumeType.Master:
                defaultValue = PlayerPrefs.GetFloat("MasterVol", 1f);
                break;
            case VolumeType.Music:
                defaultValue = PlayerPrefs.GetFloat("MusicVol", 1f);
                break;
            case VolumeType.SFX:
                defaultValue = PlayerPrefs.GetFloat("SoundFXVol", 1f);
                break;
        }

        slider.value = defaultValue;
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnSliderChanged(float value)
    {
        switch (volumeType)
        {
            case VolumeType.Master:
                SoundMixerManager.instance.SetMasterVolume(value);
                break;
            case VolumeType.Music:
                SoundMixerManager.instance.SetMusicVolume(value);
                break;
            case VolumeType.SFX:
                SoundMixerManager.instance.SetSFXVolume(value);
                break;
        }
    }
}
