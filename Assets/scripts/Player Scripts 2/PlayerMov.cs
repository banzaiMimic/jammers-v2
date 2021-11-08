using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    Animator anim;

    public GameObject landParticleEffect;
    bool canInstantiateLandParticleEffect = true;
    public GameObject feet;
    public LayerMask whatIsGround;

    public float movSpeed;
    public float jumpForce;
    public float acceleration;
    public float deacceleration;
    float hor;
    bool isGrounded;
    bool isFacingRight = true;

    float isGroundedRememberTimer;     
    float hasPressedJumpRememberTimer;
    float velocity_Y_RememberTimer;

    public float holdJumpGravity;   //Gravity when player is holding the jump button
    public float jumpGravity;      //Gravity when player has only pressed the jump button
    public float freeFallGravity;   // Gravity when player is free falling

    bool jumpOneTime = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");

        RotatePlayer();
        Jump();
        ModifyGravity();
        Animations();
        SpawnLandParticleEffects();
    }

    private void SpawnLandParticleEffects()
    {
        if (rb.velocity.y < -0.5f)
        {
            velocity_Y_RememberTimer = 0.1f;
        }
        else
        {
            velocity_Y_RememberTimer -= Time.deltaTime;
        }

        if (velocity_Y_RememberTimer > 0 && isGrounded && canInstantiateLandParticleEffect)
        {
            GameObject effect = Instantiate(landParticleEffect, feet.transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            StartCoroutine(EnableLandParticleEffectInstantiation());
            canInstantiateLandParticleEffect = false;
        }
    }

    private void Animations()
    {
        anim.SetFloat("Velocity_X", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Velocity_Y", Mathf.Abs(rb.velocity.y));
        anim.SetBool("isGrounded", isGrounded);
    }

    private void ModifyGravity()
    {
        //Implementing variable jump height
        if (rb.velocity.y > 0 && Input.GetKey(KeyCode.Space))
        {
            rb.gravityScale = holdJumpGravity;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.gravityScale = jumpGravity;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = freeFallGravity;
        }
    }

    private void RotatePlayer()
    {
        if (hor > 0 && !isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isFacingRight = true;
        }
        else if (hor < 0 && isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = false;
        }
    }

    private void Jump()
    {
        //Ground Check
        isGrounded = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.extents, 0f, Vector2.down, 0.75f, whatIsGround);

        //Using these two timers to implement Coyote Time/ Hang Time
        if (isGrounded)
        {
            isGroundedRememberTimer = 0.2f;
        }
        else
        { 
            isGroundedRememberTimer -= Time.deltaTime; 
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasPressedJumpRememberTimer = 0.2f;
        }
        else
        {
            hasPressedJumpRememberTimer -= Time.deltaTime;
        }

        if (isGroundedRememberTimer > 0 && hasPressedJumpRememberTimer > 0 && jumpOneTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            StartCoroutine(EnableJumpOneTime());
            jumpOneTime = false;
        }
    }

    void FixedUpdate()
    {
        float targetSpeed = hor * movSpeed;  //Top Speed
        float speedDif = targetSpeed - rb.velocity.x;

        if(speedDif > -0.1f && speedDif < 0.1f)
        {
            speedDif = 0f;
        }

        float accRate;   //Set the value of acceleration
        if(Mathf.Abs(targetSpeed) > 0.01f)
        {
            accRate = acceleration;
        } else
        {
            accRate = deacceleration;
        }

        float movementForce = speedDif * accRate;

        rb.AddForce(movementForce * Vector2.right);
    }

    IEnumerator EnableLandParticleEffectInstantiation()
    {
        yield return new WaitForSeconds(0.5f);
        canInstantiateLandParticleEffect = true;
    }

    IEnumerator EnableJumpOneTime()
    {
        yield return new WaitForSeconds(0.5f);
        jumpOneTime = true;
    }
}
