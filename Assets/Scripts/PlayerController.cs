using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

  public Rigidbody2D rb;
  public Transform groundCheck;
  public LayerMask groundLayer;

  private bool isFacingRight = true;
  private float horizontal;
  private float speed = 13f;
  private float jumpPower = 20f;

  // Start is called before the first frame update
  void Start() {
    
  }

  // Update is called once per frame
  void Update() {
    rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    if (!isFacingRight && horizontal > 0f) {
      Flip();
    } else if (isFacingRight && horizontal < 0f) {
      Flip();
    }
  }

  private bool IsGrounded() {
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
  }

  private void Flip() {
    isFacingRight = !isFacingRight;
    Vector3 localScale = transform.localScale;
    localScale.x *= -1f;
    transform.localScale = localScale;
  }

  public void Move(InputAction.CallbackContext context) {
    horizontal = context.ReadValue<Vector2>().x;
  }

  public void Jump(InputAction.CallbackContext context) {
    Debug.Log("jump");
    if (context.performed && IsGrounded()) {
      rb.velocity = new Vector2(rb.velocity.x, jumpPower);
    }

    if (context.canceled && rb.velocity.y > 0f) {
      rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }
  }
}
