using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerInMinAggroRange;
    protected bool isPlayerNotBlocked;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //AttackState Enter
        entity.atsm.attackState = this;
        isAnimationFinished = false;
        entity.SetVelocity(0f);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void DoChecks()
    {
        //AttackState DoChecks()
        base.DoChecks();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public virtual void PrepareAttack()
    {
        //include all preparations for the attack here.
        //may include:
        //***sprite changes
        //***animation changes, etc.
    }

    public virtual void TriggerAttack() { }

    public virtual void FinishAttack() 
    {
        isAnimationFinished = true;
    }

}
