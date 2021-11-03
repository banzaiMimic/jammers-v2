using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Idle : BaseState {

  private float _horizontalInput;

  public Idle(MovementSM stateMachine) : base("Idle", stateMachine) {
    Debug.Log("ENTER!");
    var action = new InputAction(binding: "<Gamepad>/leftStick");
    //_horizontalInput = Input.GetAxis("Horizontal");
    action.Enable(); 
    action.performed += ctx => {
      float moveX = ctx.ReadValue<Vector2>().normalized.x;
      Debug.Log("-----");
      Debug.Log("moveX:" + ctx.ReadValue<Vector2>().normalized.x);
      Debug.Log("Mathf.Abs(moveX):" + Mathf.Abs(moveX));
      Debug.Log("Mathf.Epsilon:" + Mathf.Epsilon);
      if (Mathf.Abs(moveX) > Mathf.Epsilon) {
        stateMachine.ChangeState(((MovementSM) stateMachine).movingState);
      }
    };
  }

  public override void Enter() {
    base.Enter();
    _horizontalInput = 0f;
    
    // if (action.triggered) {
    //   Debug.Log("action triggered...");
    //   stateMachine.ChangeState(((MovementSM) stateMachine).movingState);
    // }
  }

  public override void UpdateLogic() {
    base.UpdateLogic();
    //@Todo use Input System instead of old one.
    
    // transition to moving state if input != 0
    // if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon) {
    //   stateMachine.ChangeState(((MovementSM) stateMachine).movingState);
    // }
  }

}
