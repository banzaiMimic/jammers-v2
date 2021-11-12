using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_TakeDamage : MonoBehaviour
{
    public GameObject deathEffect;

    public float maxHealth;
    float health;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if(health < 0)
        {
            Die();
        }   
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }
}
