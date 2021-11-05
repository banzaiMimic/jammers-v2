using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State {

  protected SO_ChargeState stateData;
  protected bool isPlayerInMinAggroRange;
  protected bool isDetectingLedge;
  protected bool isDetectingWall;

  public ChargeState(
    Entity entity, 
    FiniteStateMachine stateMachine, 
    string animBoolName,
    SO_ChargeState stateData
  ) : base(entity, stateMachine, animBoolName) {
    this.stateData = stateData;
  }

  public override void Enter() {
    base.Enter();
    isDetectingLedge = entity.CheckLedge();
    isDetectingWall = entity.CheckWall();
    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    entity.SetVelocity(stateData.chargeSpeed);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
    isDetectingLedge = entity.CheckLedge();
    isDetectingWall = entity.CheckWall();
    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
  }

}
