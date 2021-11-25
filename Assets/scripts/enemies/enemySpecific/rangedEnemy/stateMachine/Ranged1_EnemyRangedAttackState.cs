using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged1_EnemyRangedAttackState : EnemyAttackState
{
    [SerializeField] private Player player;
    private Enemy enemy;

    //MeleeAttackState
    [SerializeField] private SO_MeleeAttackState stateData;
    private Attack dung;
    private AttackDetails attackDetails;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponentInParent<Enemy>();
        dung = enemy.GetComponentInChildren<Attack>();

        animBoolName = "rangedAttack";

        //Update state in entity
        base.OnStateEnter(animator, stateInfo, layerIndex);

        //AttackState Enter
        attackPosition = animator.gameObject.GetComponentInChildren<RangedAttackPosition>().transform;

        //RangedAttackState Enter
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

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void DoChecks()
    {
        //AttackState DoChecks()
        base.DoChecks();
        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRangeCircular();
        isPlayerNotBlocked = entity.CheckEntityIfBlocked(player.transform.position);
    }

    public override void PrepareAttack()
    {
        base.PrepareAttack();

        //flip bug


        //change bug sprite to bug without dung

        //activate the dung game object, then activate rolling

    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        //@Todo might want to let Dispatcher handle this
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("TakeDamage", enemy.basicAttack);
        }

    }

}
