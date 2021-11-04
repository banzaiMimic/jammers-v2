using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grounded : BaseState {

  // leaving movementSm here incase child classes need reference
  protected MovementSM movementSm;

  public Grounded(string name, MovementSM stateMachine) : base(name, stateMachine) {
    this.movementSm = (MovementSM) stateMachine;
  }

  public void Awake() {
    this.AddListeners();
  }

  private void AddListeners() {
    var action = new InputAction(binding: "<Gamepad>/buttonSouth");
    action.Enable(); 
    action.performed += ctx => {
      stateMachine.ChangeState(movementSm.jumpingState);
      //this.ChangeState(((MovementSM) this).movingState);
    };
    action.canceled += ctx => {
      //this.ChangeState(((MovementSM) this).idleState);
    };
  }

  public override void UpdateLogic() {
    base.UpdateLogic();
    
  }

}
