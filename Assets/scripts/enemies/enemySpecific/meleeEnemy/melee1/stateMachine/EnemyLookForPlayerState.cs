using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookForPlayerState : EnemyState
{
    [SerializeField] private SO_LookForPlayerState stateData;
    private bool flipImmediately;
    private bool isPlayerInMinAggroRange;
    private bool isAllTurnsDone;
    private bool isAllTurnsTimeDone;
    private float lastTurnTime;
    private int amountOfTurnsDone;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animBoolName = "lookForPlayer";

        //Update state in entity
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //LookForPlayerState Enter
        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;
        lastTurnTime = startTime;
        amountOfTurnsDone = 0;
        entity.SetVelocity(0f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        //LookForPlayerState Logic Update
        if (flipImmediately)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
            flipImmediately = false;
        }
        else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        if (amountOfTurnsDone >= stateData.amountOfTurns) { isAllTurnsDone = true; }
        if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone) { isAllTurnsTimeDone = true; }

        //E1_LookForPlayerState
        if (isPlayerInMinAggroRange) { ChangeState(animBoolName, "playerDetected"); }
        else if (isAllTurnsTimeDone) { ChangeState(animBoolName, "move"); }
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


}
