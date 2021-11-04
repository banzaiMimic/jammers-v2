using System.Net.Mail;
using System.Xml.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : BaseState {

  // leaving _movementSm here incase child classes need reference
  protected MovementSM _movementSm;
  private bool _grounded;
  // get ground layer since unity layer 6 is ground use bit shifting
  private int _groundLayer = 1 << 6;

  public Jumping(MovementSM stateMachine) : base("Jumping", stateMachine) {
    this._movementSm = (MovementSM) stateMachine;
  }

  public void Awake() {
    this.AddListeners();
  }

  private void AddListeners() {
    // var action = new InputAction(binding: "<Gamepad>/buttonSouth");
    // action.Enable(); 
    // action.performed += ctx => {
    //   stateMachine.ChangeState(_movementSm.jumpingState);
    //   //this.ChangeState(((MovementSM) this).movingState);
    // };
    // action.canceled += ctx => {
    //   //this.ChangeState(((MovementSM) this).idleState);
    // };
  }

  public override void Enter() {
    base.Enter();
    Vector2 vel = _movementSm.rBody.velocity;
    vel.y += _movementSm.jumpForce;
    _movementSm.rBody.velocity = vel;
  }

  public override void UpdateLogic() {
    base.UpdateLogic();
    // if _grounded change state to idle
    if (_grounded) {
      stateMachine.ChangeState(_movementSm.idleState);
    }
  }

  public override void UpdatePhysics() {
    _grounded = _movementSm.rBody.velocity.y < Mathf.Epsilon && _movementSm.rBody.IsTouchingLayers(_groundLayer);
    Debug.Log("grounded:" + _grounded);
  }

}
