using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Idle : BaseState {

  public Idle(MovementSM stateMachine) : base("Idle", stateMachine) {
    Debug.Log("ENTER!");
  }

  public override void Enter() {
    base.Enter();
  }

  public override void UpdateLogic() {
    base.UpdateLogic();
  }

}
