using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Idle : Grounded {

  public Idle(MovementSM stateMachine) : base("Idle", stateMachine) {
    Debug.Log("ENTER!");
  }

  public override void Enter() {
    base.Enter();
  }

  public override void UpdateLogic() {
    base.UpdateLogic();
  }

  public override void UpdatePhysics() {
    base.UpdatePhysics();
    Vector2 vel = movementSm.rBody.velocity;
    vel.x = 0;
    movementSm.rBody.velocity = vel;
    //movementSm.rBody.velocity.x = 0;
  }

}
