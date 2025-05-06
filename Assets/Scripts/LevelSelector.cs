using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneTransition sceneTransition;

    public Button[] levelBTNS;

    private void Start()
    {
        int lvlReached = PlayerPrefs.GetInt("lvlReached", 1);

        for (int i = 0; i < levelBTNS.Length; i++)
        {
            if (i + 1 > lvlReached) { 
            levelBTNS[i].interactable = false;
            }
        }
    }

    public void Select(string levelName)
    {
        sceneTransition.TransitionTo(levelName);
    }
}
