using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetectedState : EnemyState
{
    [SerializeField] private SO_PlayerDetectedState stateData;
    private bool isPlayerInMinAggroRange;
    private bool isPlayerInMaxAggroRange;
    private bool performLongRangeAction;
    private bool performCloseRangeAction;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animBoolName = "playerDetected";

        //Update state in entity
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //PlayerDetectedState Enter
        performLongRangeAction = false;
        entity.SetVelocity(0f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //PlayerDetected State Logic Update
        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }

        //E1_PlayerDetectedState Logic Update
        if (performCloseRangeAction) { ChangeState(animBoolName, "meleeAttack"); }
        else if (performLongRangeAction) { ChangeState(animBoolName, "charge"); }
        else if (!isPlayerInMaxAggroRange) { ChangeState(animBoolName, "lookForPlayer"); }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRange();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }
}
