using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;
    public AudioClip bulletSFX;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireCountdown = 0f;
    public float firerate = 1f;

    [Header("Use Laser")]
    public AudioClip laserSFX;
    private AudioSource laserAudioSource;
    public bool userLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public float dps = 29.1f;
    public float slowRate = .5f;


    [Header("Unity Setup Fields")]

    public Transform rotatingPart;
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    public Transform firePoint;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .5f);
    }

    
    void Update()
    {
        if (target == null) {

            if (userLaser) { 
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;


                    if (laserAudioSource != null)
                    {
                        Destroy(laserAudioSource.gameObject); // This stops and cleans up the AudioSource
                        laserAudioSource = null;
                    }
                }
            }

            return;
        }
        LockOnTarget();

        if (userLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / firerate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(dps * Time.deltaTime);
        targetEnemy.Slow(slowRate);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        if (laserSFX != null && laserAudioSource == null)
        {
            laserAudioSource = SoundsManager.instance.PlayLoopingSoundFX(laserSFX, firePoint);
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);   

        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);


    }

    public void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null) {
            bullet.Seek(target);
        }

        if (bulletSFX != null)
        {
            SoundsManager.instance.PlaySoundFX(bulletSFX, firePoint);
        }

    }

    public void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) { 
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance) { 
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

        }

        if (nearestEnemy != null && shortestDistance <= range) { 
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        } else
        {
            target = null;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void OnDestroy()
    {
        if (laserAudioSource != null)
        {
            Destroy(laserAudioSource.gameObject);
            laserAudioSource = null;
        }
    }

}
