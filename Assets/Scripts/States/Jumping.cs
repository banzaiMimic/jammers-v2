using System.Xml.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : BaseState {

  // leaving movementSm here incase child classes need reference
  protected MovementSM movementSm;
  private bool _grounded;
  // get ground layer since unity layer 6 is ground use bit shifting
  private int _groundLayer = 1 << 6;

  public Jumping(MovementSM stateMachine) : base("Jumping", stateMachine) {
    this.movementSm = (MovementSM) stateMachine;
  }

  public void Awake() {
    this.AddListeners();
  }

  private void AddListeners() {
    // var action = new InputAction(binding: "<Gamepad>/buttonSouth");
    // action.Enable(); 
    // action.performed += ctx => {
    //   stateMachine.ChangeState(movementSm.jumpingState);
    //   //this.ChangeState(((MovementSM) this).movingState);
    // };
    // action.canceled += ctx => {
    //   //this.ChangeState(((MovementSM) this).idleState);
    // };
  }

  public override void Enter() {
    base.Enter();
    // Vector2 vel = movementSm.rigidBody.velocity;
    // vel.y += movementSm.jumpForce;
    //movementSm.rigidBody.velocity = vel;
  }

  public override void UpdateLogic() {
    base.UpdateLogic();
    // if _grounded change state to idle
  }

  public override void UpdatePhysics() {
    //_grounded = movementSm.rigidBody.velocity.y < Mathf.Epsilon && movementSm.rigidBody.IsTouchingLayers(_groundLayer);
  }

}
