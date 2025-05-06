using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;
    [SerializeField] private AudioSource soundFXObj;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Get the saved linear SFX volume from PlayerPrefs
    private float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat("SoundFXVol", 1f); 
    }

    public void PlaySoundFX(AudioClip clip, Transform spawnTrans, float overrideVolume = -1f)
    {
        float volume = overrideVolume >= 0f ? overrideVolume : GetSFXVolume();

        AudioSource audioSource = Instantiate(soundFXObj, spawnTrans.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(audioSource.gameObject, clip.length);
    }

    public AudioSource PlayLoopingSoundFX(AudioClip clip, Transform spawnTrans, float overrideVolume = -1f)
    {
        float volume = overrideVolume >= 0f ? overrideVolume : GetSFXVolume();

        AudioSource audioSource = Instantiate(soundFXObj, spawnTrans.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();

        return audioSource;
    }
}

