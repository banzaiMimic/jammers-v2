using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSM : StateMachine {

  [HideInInspector]
  public Idle idleState;
  [HideInInspector]
  public Moving movingState;
  [HideInInspector]
  public Jumping jumpingState;
  [HideInInspector] 
  public Vector2 movementInput { get; private set; }
  [HideInInspector]
  public float horizontalVelocity = 0f;

  public Rigidbody2D rBody;
  public float speed = 4f;
  public float jumpForce = 20f;

  private void Awake() {
    this.AddListeners();
    this.idleState = new Idle(this);
    this.movingState = new Moving(this);
    this.jumpingState = new Jumping(this);
    this.rBody = GetComponent<Rigidbody2D>();
  }

  //@Todo might want to call this only in our Player ? 
  private void AddListeners() {
    var action = new InputAction(binding: "<Gamepad>/leftStick");
    action.Enable(); 
    action.performed += ctx => {
      this.movingState.MoveEntity(ctx);
      this.ChangeState(((MovementSM) this).movingState);
    };
    action.canceled += ctx => {
      this.ChangeState(((MovementSM) this).idleState);
    };
  }

  protected override BaseState GetInitialState() {
    return idleState;
  }

}
