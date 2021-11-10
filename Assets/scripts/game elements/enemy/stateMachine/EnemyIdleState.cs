using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Entity_ entity;

    //state-specific
    [SerializeField] private SO_IdleState stateData;
    private bool flipAfterIdle;
    private bool isIdleTimeOver;
    private bool isPlayerInMinAggroRange;
    private float idleTime;
    
    private float startTime;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        entity = animator.gameObject.GetComponent<Entity_>();

        //State Enter
        startTime = Time.time;
        entity.animator.SetBool("idle", true);
        DoChecks();

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
            if (flipAfterIdle)
            {
                entity.Flip();
            }

            //State Exit
            entity.animator.SetBool("idle", false);
            entity.animator.SetBool("playerDetected", true);
        }
        else if (isIdleTimeOver)
        {
            entity.animator.SetBool("idle", false);
            entity.animator.SetBool("move", true);
        }
    }

    public override void OnFixedUpdate()
    {
        DoChecks();
    }

    public void DoChecks()
    {
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
