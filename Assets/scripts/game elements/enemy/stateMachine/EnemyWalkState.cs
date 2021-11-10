using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkState : EnemyState
{
    
    private Entity_ entity;

    [SerializeField] private SO_MoveState stateData;
    private bool isDetectingWall;
    private bool isDetectingLedge;
    private bool isPlayerInMinAggroRange;

    private float startTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        entity = animator.gameObject.GetComponent<Entity_>();

        //State Enter
        startTime = Time.time;
        entity.animator.SetBool("move", true);
        DoChecks();

        //Move State Enter
        entity.SetVelocity(stateData.movementSpeed);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //E1_MoveState Logic Update

        if (isPlayerInMinAggroRange)
        {
            //State Exit
            entity.animator.SetBool("move", false);
            entity.animator.SetBool("playerDetected", true);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
    public override void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    private void DoChecks()
    {
        //Move State DoChecks
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }
}
