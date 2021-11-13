using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeState : EnemyState
{
    [SerializeField] private SO_ChargeState stateData;
    private bool isPlayerInMinAggroRange;
    private bool isDetectingLedge;
    private bool isDetectingWall;
    private bool isChargeTimeOver;
    private bool performCloseRangeAction;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animBoolName = "charge";

        //Update state in entity
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //ChargeState Enter
        isChargeTimeOver = false;
        entity.SetVelocity(stateData.chargeSpeed);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        //ChargeState Logic Update
        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }

        //E1_ChargeState Logic Update
        if (performCloseRangeAction) { ChangeState(animBoolName, "meleeAttack"); }
        else if (!isDetectingLedge || isDetectingWall) { ChangeState(animBoolName, "lookForPlayer"); }
        else if (isChargeTimeOver)
        {
            if (isPlayerInMinAggroRange) { ChangeState(animBoolName, "playerDetected"); }
            else { ChangeState(animBoolName, "lookForPlayer"); }
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }
}
