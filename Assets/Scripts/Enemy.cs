using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float startSpeed = 20f;
    [HideInInspector]
    public float speed;



    public float startHealth = 100;
    private float health;
    public int damage = 1;

    public int dropMoney = 50;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health/startHealth;

        if (health <= 0 && !isDead)
        {
            Defeated();
        }
    }

    public void Slow(float slowRate)
    {
        speed = startSpeed * (1f - slowRate);
    }

    void Defeated()
    {
        isDead = true;

        PlayerStats.Money += dropMoney;

        GameObject effect =  Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);

        WaveSpawner.EnemiesAlive--;
      
        Destroy(gameObject);
    }
}
