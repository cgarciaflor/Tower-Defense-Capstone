using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public string levelToLoad = "Main Menu";
    public SceneTransition sceneTrans;
    public void BackToMenu()
    {
        sceneTrans.TransitionTo(levelToLoad);
    }
    public void Quit()
    {
        Debug.Log("Exciting.....");
        Application.Quit();
    }
}
