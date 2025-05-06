using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnpoint;

    public TMPro.TextMeshProUGUI waveCountdownText;

    public GameManager manager;

    public float timeBetweenWave = 5.5f; 
    private float countdown = 2f;

    private int waveNum = 0;

    private void Start()
    {
        EnemiesAlive = 0;
    }


    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveNum == waves.Length)
        {
            manager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWave;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveNum];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveNum++;
    }

    public void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnpoint.position, spawnpoint.rotation);
    }
}
