using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged1_EnemyWalkState : EnemyState
{
    private Player player;

    [SerializeField] private SO_MoveState stateData;
    private bool isDetectingWall;
    private bool isDetectingLedge;
    private bool isPlayerInMinAggroRange;
    protected bool isPlayerNotBlocked;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = Player.Instance;

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

        Debug.Log(isPlayerInMinAggroRange);
        Debug.Log(isPlayerNotBlocked);


        //E1_MoveState Logic Update
        if (isPlayerInMinAggroRange && isPlayerNotBlocked)
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
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRangeCircular();
        isPlayerNotBlocked = !entity.CheckEntityIfBlocked(player.transform.position);
    }
}
