using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    public static SoundMixerManager instance;

    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadVolumes();
    }
    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(Mathf.Clamp(level, 0.0001f, 1f)) * 20f);
        PlayerPrefs.SetFloat("MasterVol", level);
    }

    public void SetSFXVolume(float level)
    {
        audioMixer.SetFloat("SoundFXVol", Mathf.Log10(Mathf.Clamp(level, 0.0001f, 1f)) * 20f);
        PlayerPrefs.SetFloat("SoundFXVol", level);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(Mathf.Clamp(level, 0.0001f, 1f)) * 20f);
        PlayerPrefs.SetFloat("MusicVol", level);
    }

    public void LoadVolumes()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("MasterVol", 1f));
        SetSFXVolume(PlayerPrefs.GetFloat("SoundFXVol", 1f));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVol", 1f));
    }
}

