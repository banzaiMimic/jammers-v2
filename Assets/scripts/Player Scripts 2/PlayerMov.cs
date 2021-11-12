using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMov : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    Animator anim;
    
    public GameObject landParticleEffect;
    bool canInstantiateLandParticleEffect = true;
    public GameObject feet;
    public LayerMask whatIsGround;

    [Header("Player Movement")]
    public float movSpeed;
    public float jumpForce;
    public float acceleration;
    public float deacceleration;

    [Header("Dash")]
    public float dashTime;
    public float dashSpeed;
    public float dashDeccelarationTime;
    public float dashCooldownTime;
    float currentDashSpeed;
    float dashTimer;
    float dashLerpTimer;
    bool isDashing;
    bool canDash;

    float hor;

    bool isGrounded;
    bool isFacingRight = true;

    [Header("Recoil")]
    public GameObject slash;
    public Vector2 recoilForce;
    bool recoil;

    //Timers to check if isGrounded/HasPressedJump/(rb.velocity.y < 0) was true in last 0.2 seconds
    float isGroundedRememberTimer;     
    float hasPressedJumpRememberTimer;
    float velocity_Y_RememberTimer;

    [Header("Modify Gravity for jumps")]
    public float holdJumpGravity;   //Gravity when player is holding the jump button
    public float jumpGravity;      //Gravity when player has only pressed the jump button
    public float freeFallGravity;   // Gravity when player is free falling

    bool jumpOneTime = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        dashTimer = dashTime;
        currentDashSpeed = dashSpeed;
        canDash = true;
        isDashing = false;
    }

    void Update()
    {
        recoil = slash.GetComponent<GiveDamage>().doRecoil;
        if (recoil)
        {
            float recoilDirection = isFacingRight ? -1 : 1;
            rb.AddForce(new Vector2(recoilForce.x * recoilDirection, recoilForce.y), ForceMode2D.Impulse);
            recoil = false;
        }

        hor = Input.GetAxisRaw("Horizontal");

        RotatePlayer();
        Jump();
        ModifyGravity();
        Animations();
        SpawnLandParticleEffects();
        Dash();
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !recoil)
        {
            isDashing = true;
        }

        if (isDashing)
        {
            rb.velocity = isFacingRight ? new Vector2(currentDashSpeed, 0f) : new Vector2(-currentDashSpeed, 0f);

            dashTimer -= Time.deltaTime;
            if (dashTimer < 0)
            {
                dashLerpTimer += Time.deltaTime;
                dashLerpTimer = Mathf.Clamp(dashLerpTimer, 0, 1);

                print(dashLerpTimer);
                currentDashSpeed = Mathf.Lerp(currentDashSpeed, movSpeed - 2, dashLerpTimer);

                if (currentDashSpeed <= movSpeed)
                {
                    StartCoroutine(enableCanDash());

                    dashTimer = dashTime;
                    dashLerpTimer = 0f;
                    isDashing = false;
                    currentDashSpeed = dashSpeed;
                    canDash = false;
                    rb.velocity = Vector2.zero;
                }
            }
        }
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

        if (velocity_Y_RememberTimer > 0 && isGrounded && canInstantiateLandParticleEffect && !isDashing)
        {
            GameObject effect = Instantiate(landParticleEffect, feet.transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            StartCoroutine(EnableLandParticleEffectInstantiation());
            canInstantiateLandParticleEffect = false;
        }
    }

    private void Animations()
    {
        if (!isDashing)
        {
            anim.SetFloat("Velocity_X", Mathf.Abs(rb.velocity.x));
            anim.SetFloat("Velocity_Y", Mathf.Abs(rb.velocity.y));
            anim.SetBool("isGrounded", isGrounded);
        }
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
        if (!isDashing && !recoil)
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

        if (isGroundedRememberTimer > 0 && hasPressedJumpRememberTimer > 0 && jumpOneTime && !isDashing && !recoil)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            StartCoroutine(EnableJumpOneTime());
            jumpOneTime = false;
        }
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (!isDashing && !recoil)
        {
            float targetSpeed = hor * movSpeed;  //Top Speed
            float speedDif = targetSpeed - rb.velocity.x;

            if (speedDif > -0.1f && speedDif < 0.1f)
            {
                speedDif = 0f;
            }

            float accRate;   //Set the value of acceleration
            if (Mathf.Abs(targetSpeed) > 0.01f)
            {
                accRate = acceleration;
            }
            else
            {
                accRate = deacceleration;
            }

            float movementForce = speedDif * accRate;

            rb.AddForce(movementForce * Vector2.right);
        }
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

    IEnumerator enableCanDash()
    {
        yield return new WaitForSeconds(dashCooldownTime);
        canDash = true;
    }
}
