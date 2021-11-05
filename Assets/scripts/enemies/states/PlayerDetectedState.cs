using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State {

  protected SO_PlayerDetectedState stateData;
  protected bool isPlayerInMinAggroRange;
  protected bool isPlayerInMaxAggroRange;

  public PlayerDetectedState(
    Entity entity, 
    FiniteStateMachine stateMachine, 
    string animBoolName,
    SO_PlayerDetectedState stateData
  ): base(entity, stateMachine, animBoolName) {
    this.stateData = stateData;
  }

  public override void Enter() {
    base.Enter();
    entity.SetVelocity(0f);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
  }

}
