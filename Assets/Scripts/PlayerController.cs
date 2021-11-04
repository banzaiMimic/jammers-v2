using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

  [SerializeField] 
  private LayerMask jumpableGround;

  [HideInInspector] 
  public Vector2 movementInput { get; private set; }

  private Rigidbody2D rBody;
  private BoxCollider2D bCollider;

  private bool isFacingRight = true;
  private float horizontal;
  private float speed = 4f;
  private float jumpPower = 20f;

  void Start() {
    rBody = GetComponent<Rigidbody2D>();
    bCollider = GetComponent<BoxCollider2D>();
  }

  void Update() {
    rBody.velocity = new Vector2(horizontal * speed, rBody.velocity.y);

    if (!isFacingRight && horizontal > 0f) {
      Flip();
    } else if (isFacingRight && horizontal < 0f) {
      Flip();
    }
  }

  private bool IsGrounded() {
    return Physics2D.BoxCast(bCollider.bounds.center, bCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
  }

  private void Flip() {
    isFacingRight = !isFacingRight;
    Vector3 localScale = transform.localScale;
    localScale.x *= -1f;
    transform.localScale = localScale;
  }

  public void Move(InputAction.CallbackContext context) {
    movementInput = context.ReadValue<Vector2>();
    float moveX = movementInput.x;
    if (moveX > 0) {
      horizontal = speed;
    } else if (moveX < 0) {
      horizontal = -speed;
    } else {
      horizontal = 0f;
    }
  }

  public void Jump(InputAction.CallbackContext context) {
    Debug.Log("jump");
    if (context.performed && IsGrounded()) {
      rBody.velocity = new Vector2(rBody.velocity.x, jumpPower);
    }

    if (context.canceled && rBody.velocity.y > 0f) {
      rBody.velocity = new Vector2(rBody.velocity.x, rBody.velocity.y * 0.5f);
    }
  }
}
