using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSM : StateMachine {

  [HideInInspector]
  public Idle idleState;
  [HideInInspector]
  public Moving movingState;

  private void Awake() {
    this.addListeners();
    idleState = new Idle(this);
    movingState = new Moving(this);
  }

  //@Todo might want to call this only in our Player ? 
  private void addListeners() {
    var action = new InputAction(binding: "<Gamepad>/leftStick");
    action.Enable(); 
    action.performed += ctx => {
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
