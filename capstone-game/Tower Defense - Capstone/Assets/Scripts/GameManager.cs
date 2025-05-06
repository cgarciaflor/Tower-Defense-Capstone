using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject completeLVLUI;

    public AudioClip gameOverSFX;
    public AudioClip gameWinSFX;

    void Start()
    {
        Time.timeScale = 1f;
        GameIsOver = false;
    }

    
    void Update()
    {
        if (GameIsOver) { return; }
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }

    // for testing
    if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
        if (gameOverSFX != null)
        {
            SoundsManager.instance.PlaySoundFX(gameOverSFX, transform);
        }
    }

    public void WinLevel()
    {
        GameIsOver = true;
        completeLVLUI.SetActive(true);
        if (gameWinSFX != null)
        {
            SoundsManager.instance.PlaySoundFX(gameWinSFX, transform);
        }
    }
}
