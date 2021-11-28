using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Rigidbody2D attackRB = null;
    private Animator animator = null;

    public float DamageAmount { get; set; } = 1f;
    private float bulletSpeed = 0f;
    private float maxTravel = 0f;
    private Vector2 source;
    private bool isFacingRight = false;

    //dung projectile
    private float airTime = 2f;
    private float launchForce;

    private void Awake()
    {
        //components
        attackRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //attack-related parameters
        bulletSpeed = 2f;
        maxTravel = 3f;
        source = transform.position;
        isFacingRight = true;
    }



    private void Update()
    {
        /*
        //destroys bullet upon travelling max length
        if (Vector3.Magnitude(transform.position - new Vector3(source.x, source.y)) >= 3f)
        {
            Destroy(gameObject);
        }
        */
    }

    private void Start()
    {
        float sign = isFacingRight ? 1 : -1;

        Vector3 Vo = CalculateVelocity();
        attackRB.velocity = Vo;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if bullet collides with an enemy before reaching max length, bullet will be destroyed
        if (collision.transform.tag == "Player")
        {
            //do damage to the collided enemy
            if (collision.gameObject.TryGetComponent(out Base element) && element as Player)
            {
                element = element as Player;
                bool isDead = element.TakeDamage(DamageAmount);
                Debug.Log($"Is enemy dead? {isDead}");
            }

            Destroy(gameObject);
        }
    }

    private Vector3 CalculateVelocity()
    {
        //this method will be called at Start upon instantiation
        Vector3 target = Player.Instance.transform.position;
        Vector3 origin = transform.position;

        Vector3 distance = target - origin;
        Vector3 distanceXZ = Vector3.Normalize(distance);
        distanceXZ.y = 0f;
        float Sy = distance.y;
        float Sxz = distance.magnitude;

        //velocity
        float Vxz = Sxz / airTime;
        float Vy = Sy / airTime + 0.5f * Mathf.Abs(Physics.gravity.y) * airTime;

        Vector3 velocity = distanceXZ * Vxz;
        velocity.y = Vy;

        return velocity;
    }
}