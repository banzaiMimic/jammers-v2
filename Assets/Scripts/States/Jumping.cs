using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : BaseState {

  // leaving _movementSm here incase child classes need reference
  protected MovementSM _movementSm;
  private bool _grounded;
  // get ground layer since unity layer 6 is ground use bit shifting
  private int _groundLayer = 1 << 6;

  public Jumping(MovementSM stateMachine) : base("Jumping", stateMachine) {
    this._movementSm = (MovementSM) stateMachine;
  }

  public override void Enter() {
    base.Enter();
    Vector2 vel = _movementSm.rBody.velocity;
    vel.y += _movementSm.jumpForce;
    _movementSm.rBody.velocity = vel;
  }

  public override void Update() {
    base.Update();
    Debug.Log("_movementSm.rBody.velocity.y : " + _movementSm.rBody.velocity.y + "Mathf.Epsilon : " + Mathf.Epsilon);
    Debug.Log("_movementSm.rBody.IsTouchingLayers(_groundLayer) : " + _movementSm.rBody.IsTouchingLayers(_groundLayer));
    Debug.Log("---");
    _grounded = _movementSm.rBody.velocity.y < Mathf.Epsilon && _movementSm.rBody.IsTouchingLayers(_groundLayer);
    Debug.Log("grounded:" + _grounded + " groundLayer: " + _groundLayer);
    // if _grounded change state to idle
    if (_grounded) {
      Debug.Log("[_grounded!] changing to idle...");
      stateMachine.ChangeState(_movementSm.idleState);
    }
  }

  public override void LateUpdate() {
    base.LateUpdate();
  }

}
