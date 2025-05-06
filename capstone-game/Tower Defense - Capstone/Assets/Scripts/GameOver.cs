using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public SceneTransition sceneTrans;

    public string mainMenuSceneName = "Main Menu";


    public void Retry()
    {
        sceneTrans.TransitionTo(SceneManager.GetActiveScene().name);
        
    }

    public void Menu()
    {
        sceneTrans.TransitionTo(mainMenuSceneName);
    }
}
