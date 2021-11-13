using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyIdleState : EnemyState
{
    [SerializeField] private SO_IdleState stateData;
    private bool isIdleTimeOver;
    private bool isPlayerInMinAggroRange;
    private float idleTime;
    
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animBoolName = "idle";

        //Update state in entity
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //Idle State Enter
        entity.SetVelocity(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Idle State Logic Update
        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }

        //E1_IdleState Logic Update
        if (isPlayerInMinAggroRange)
        {
            //Idle State Exit
            if (entity.flipAfterIdle)
            {
                entity.Flip();
            }

            //State Exit
            ChangeState(animBoolName, "playerDetected");
        }
        else if (isIdleTimeOver)
        {
            //Idle State Exit
            if (entity.flipAfterIdle)
            {
                entity.Flip();
            }

            //State Exit
            ChangeState(animBoolName, "move"); 
        }
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
