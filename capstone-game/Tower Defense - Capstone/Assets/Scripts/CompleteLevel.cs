using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public SceneTransition sceneTransition;

    public string mainMenuSceneName = "Main Menu";

    public string nextLVL = "Level Two";
    public int LVLToUnlock = 2;

    public void Continue()
    {
        PlayerPrefs.SetInt("lvlReached", LVLToUnlock);
        sceneTransition.TransitionTo(nextLVL);
    }

    public void Menu()
    {
        sceneTransition.TransitionTo(mainMenuSceneName);
    }
}
