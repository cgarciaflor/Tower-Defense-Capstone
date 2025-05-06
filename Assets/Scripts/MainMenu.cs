using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "Level Select Menu";
    public string creditsScene = "End Credits";
    public SceneTransition sceneTrans;
    public GameObject settingsPanel;
    public void Play()
    {
        sceneTrans.TransitionTo(levelToLoad);
    }
    public void Quit()
    {
        Debug.Log("Exciting.....");
        Application.Quit();
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void ToCredits()
    {
        sceneTrans.TransitionTo(creditsScene);
    }
}
