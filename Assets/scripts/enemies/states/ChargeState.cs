using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State {

  protected SO_ChargeState stateData;
  protected bool isPlayerInMinAggroRange;
  protected bool isDetectingLedge;
  protected bool isDetectingWall;
  protected bool isChargeTimeOver;

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
    isChargeTimeOver = false;
    entity.SetVelocity(stateData.chargeSpeed);
  }

  public override void Exit() {
    base.Exit();
  }

  public override void LogicUpdate() {
    base.LogicUpdate();
    if (Time.time >= startTime + stateData.chargeTime) {
      isChargeTimeOver = true;
    }
  }

  public override void PhysicsUpdate() {
    base.PhysicsUpdate();
  }

  public override void DoChecks() {
    base.DoChecks();
    isDetectingLedge = entity.CheckLedge();
    isDetectingWall = entity.CheckWall();
    isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
  }

}
