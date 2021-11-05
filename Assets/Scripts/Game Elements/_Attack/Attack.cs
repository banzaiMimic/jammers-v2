using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Rigidbody2D attackRB = null;

    private float bulletSpeed = 0f;
    private float maxTravel = 0f;
    private bool isFacingRight = false;

    private void Awake()
    {
        bulletSpeed = 2f;
        maxTravel = 3f;
    }

    private void Start()
    {
        float sign = isFacingRight ? 1 : -1;
        attackRB.velocity = new Vector2(sign * bulletSpeed, attackRB.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
