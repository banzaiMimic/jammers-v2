using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Entity_ enemy;

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
        enemy = animator.gameObject.GetComponent<Entity_>();

        //State Enter
        startTime = Time.time;
        enemy.animator.SetBool("idle", true);
        DoChecks();

        //Idle State Enter
        enemy.SetVelocity(0f);
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
                enemy.Flip();
            }

            //State Exit
            enemy.animator.SetBool("idle", false);
            enemy.animator.SetBool("playerDetected", true);
        }
        else if (isIdleTimeOver)
        {
            enemy.animator.SetBool("idle", false);
            enemy.animator.SetBool("move", true);
        }
    }

    public override void OnFixedUpdate()
    {
        DoChecks();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    public void DoChecks()
    {
        isPlayerInMinAggroRange = enemy.CheckPlayerInMinAggroRange();
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
