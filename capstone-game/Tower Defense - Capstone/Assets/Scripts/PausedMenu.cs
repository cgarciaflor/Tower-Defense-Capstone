using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject settingsMenu;
    public SceneTransition sceneTrans;
    public string mainMenuSceneName = "Main Menu";
    public AudioMixer masterMixer;

    private bool sfxMutedForPause = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (settingsMenu.activeSelf)
        {
            settingsMenu.SetActive(false);
            return;
        }

        menu.SetActive(!menu.activeSelf);

        if (menu.activeSelf)
        {
            Time.timeScale = 0f;

            // Only mute if not already muted
            if (masterMixer.GetFloat("SoundFXVol", out float currentVolume))
            {
                if (currentVolume > -79f)
                {
                    masterMixer.SetFloat("SoundFXVol", -80f);
                    sfxMutedForPause = true;
                }
            }
        }
        else
        {
            Time.timeScale = 1f;

            if (sfxMutedForPause)
            {
                // Get saved linear volume and convert to dB
                float linearVolume = PlayerPrefs.GetFloat("SoundFXVol", 1f); // default to full volume
                float volumeDb = Mathf.Log10(Mathf.Max(linearVolume, 0.0001f)) * 20f;
                masterMixer.SetFloat("SoundFXVol", volumeDb);
                sfxMutedForPause = false;
            }

            settingsMenu.SetActive(false);
        }
    }

    public void Retry()
    {
        Toggle();
        sceneTrans.TransitionTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneTrans.TransitionTo(mainMenuSceneName);
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
    }
}

