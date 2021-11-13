using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : EnemyState
{
    [SerializeField] private SO_MoveState stateData;
    private bool isDetectingWall;
    private bool isDetectingLedge;
    private bool isPlayerInMinAggroRange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        animBoolName = "move";

        //Update state in entity
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //Move State Enter
        entity.SetVelocity(stateData.movementSpeed);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        //E1_MoveState Logic Update
        if (isPlayerInMinAggroRange)
        {
            //State Exit
            ChangeState(animBoolName, "playerDetected");
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            entity.SetFlipAfterIdle(true);
            ChangeState(animBoolName, "idle");
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        //Move State DoChecks
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }
}
