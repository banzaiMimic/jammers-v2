using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

  public Rigidbody2D rigidBody2D;
  public float moveSpeed = 13;

  private float inputX;

  // Start is called before the first frame update
  void Start() {
    
  }

  // Update is called once per frame
  void Update() {
    rigidBody2D.velocity = new Vector2(inputX * moveSpeed, rigidBody2D.velocity.y);
  }

  public void Move(InputAction.CallbackContext context) {
    inputX = context.ReadValue<Vector2>().x;
  }
}
