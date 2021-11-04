using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Moving : Grounded {

  public Moving(MovementSM stateMachine) : base("Moving", stateMachine) {}

  public override void Enter() {
    base.Enter();
  }

  public override void UpdateLogic() {
    base.UpdateLogic();
    //_horizontalInput = Input.GetAxis("Horizontal");
    // transition to idle state if input = 0
    // if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon) {
    //   stateMachine.ChangeState(((MovementSM) stateMachine).idleState);
    // }
  }

  public override void UpdatePhysics() {
    base.UpdatePhysics();
    Vector2 vel = movementSm.rBody.velocity;
    vel.x = movementSm.horizontalVelocity * movementSm.speed;
    movementSm.rBody.velocity = vel;
  }

  public void MoveEntity(InputAction.CallbackContext context) {
    Vector2 movementInput = context.ReadValue<Vector2>();
    float moveX = movementInput.x;
    Debug.Log("moveX:" + moveX);
    if (moveX > 0) {
      movementSm.horizontalVelocity = 1;
    } else if (moveX < 0) {
      movementSm.horizontalVelocity = -1;
    } else {
      movementSm.horizontalVelocity = 0f;
    }
  }

}
