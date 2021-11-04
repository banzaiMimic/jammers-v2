using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Moving : Grounded {

  public Moving(MovementSM stateMachine) : base("Moving", stateMachine) {}

  public override void Enter() {
    base.Enter();
  }

  public override void Update() {
    base.Update();
  }

  public override void LateUpdate() {
    base.LateUpdate();
    Vector2 vel = movementSm.rBody.velocity;
    vel.x = movementSm.horizontalVelocity * movementSm.speed;
    movementSm.rBody.velocity = vel;

    if (!movementSm.isFacingRight && movementSm.horizontalVelocity > 0f) {
      Flip();
    } else if (movementSm.isFacingRight && movementSm.horizontalVelocity < 0f) {
      Flip();
    }
  }

  private void Flip() {
    movementSm.isFacingRight = !movementSm.isFacingRight;
    Vector3 localScale = movementSm.player.transform.localScale;
    localScale.x *= -1f;
    movementSm.player.transform.localScale = localScale;
  }

  public void MoveEntity(InputAction.CallbackContext context) {
    Vector2 movementInput = context.ReadValue<Vector2>();
    float moveX = movementInput.x;
    if (moveX > 0) {
      movementSm.horizontalVelocity = 1;
    } else if (moveX < 0) {
      movementSm.horizontalVelocity = -1;
    } else {
      movementSm.horizontalVelocity = 0f;
    }
  }

}
