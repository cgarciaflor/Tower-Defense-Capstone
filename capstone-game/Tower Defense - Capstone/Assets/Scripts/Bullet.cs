using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 50f;

    public GameObject impactEffect;
    public float damageRadius = 0f;

    public int damage = 50;
    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float disThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= disThisFrame) {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * disThisFrame, Space.World);
        transform.LookAt(target);

    }

    public void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);

        if (damageRadius > 0f) {
            Explode();
        } else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    public void Explode()
    {
       Collider[] hitObj =  Physics.OverlapSphere(transform.position, damageRadius);
        foreach (Collider col in hitObj) { 
            if (col.tag == "Enemy")
            {
                Damage(col.transform);
            }
        }
    }

    public void Damage (Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
