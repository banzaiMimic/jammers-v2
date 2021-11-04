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
    movementSm.rBody.velocity = new Vector2(0, 0);
  }

}
