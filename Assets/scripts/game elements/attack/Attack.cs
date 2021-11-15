using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Rigidbody2D attackRB = null;

    public float DamageAmount { get; set; } = 0f;
    private float bulletSpeed = 0f;
    private float maxTravel = 0f;
    private Vector2 source;
    private bool isFacingRight = false;

    private void Awake()
    {
        //components
        attackRB = GetComponent<Rigidbody2D>();

        //attack-related parameters
        bulletSpeed = 2f;
        maxTravel = 3f;
        source = transform.position;
        isFacingRight = true;
    }

    private void Update()
    {
        //destroys bullet upon travelling max length
        if (Vector3.Magnitude(transform.position - new Vector3(source.x, source.y)) >= 3f)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        float sign = isFacingRight ? 1 : -1;
        attackRB.velocity = new Vector2(sign * bulletSpeed, attackRB.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if bullet collides with an enemy before reaching max length, bullet will be destroyed
        if (collision.transform.tag == "Enemy")
        {
            //do damage to the collided enemy
            if (TryGetComponent<Base>(out Base element) && element as Enemy)
            {
                bool isDead = element.TakeDamage(DamageAmount);
                Debug.Log($"Is enemy dead? {isDead}");
            }

            Destroy(gameObject);
        }
    }
}
