using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged1_EnemyPlayerDetectedState : EnemyState
{
    [SerializeField] private SO_PlayerDetectedState stateData;
    private bool isPlayerInMinAggroRange;
    private bool isPlayerInMaxAggroRange;
    protected bool isPlayerNotBlocked;
    private bool performLongRangeAction;

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
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        Debug.Log(isPlayerInMinAggroRange);

        //PlayerDetected State Logic Update
        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            //longRangeActionTime will be shorter for ranged enemies
            performLongRangeAction = true;
        }

        //E1_PlayerDetectedState Logic Update
        if (!isPlayerInMaxAggroRange ) { ChangeState(animBoolName, "lookForPlayer"); }
        else if (isPlayerInMinAggroRange) { ChangeState(animBoolName, "rangedAttack"); }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRangeCircular();
        isPlayerInMaxAggroRange = entity.CheckPlayerInMaxAggroRangeCircular();
    }
}
