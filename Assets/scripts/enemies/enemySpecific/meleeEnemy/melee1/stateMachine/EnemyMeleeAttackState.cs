using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyState
{
    private Enemy enemy;

    //MeleeAttackState
    [SerializeField] private SO_MeleeAttackState stateData;
    private AttackDetails attackDetails;

    //AttackState
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerInMinAggroRange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponentInParent<Enemy>();

        animBoolName = "meleeAttack";

        //Update state in entity
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //AttackState Enter
        attackPosition = animator.gameObject.GetComponentInChildren<MeleeAttackPosition>().transform;
        entity.atsm.attackState = this;
        isAnimationFinished = false;
        entity.SetVelocity(0f);

        //MeleeAttackState Enter
        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = entity.aliveGO.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        //E1_LookForPlayerState
        if (isAnimationFinished)
        {
            if (isPlayerInMinAggroRange) { ChangeState(animBoolName, "playerDetected"); }
            else { ChangeState(animBoolName, "lookForPlayer"); }
        }
    }

    public override void DoChecks()
    {
        //AttackState DoChecks()
        base.DoChecks();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public void FinishAttack()
    {
        isAnimationFinished = true;
    }

    public void TriggerAttack()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        //@Todo might want to let Dispatcher handle this
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("TakeDamage", enemy.basicAttack);
        }
    }

}
