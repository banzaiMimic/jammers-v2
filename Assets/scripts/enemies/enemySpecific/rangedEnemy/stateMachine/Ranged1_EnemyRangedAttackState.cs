using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged1_EnemyRangedAttackState : EnemyAttackState
{
    public Enemy enemy;

    //MeleeAttackState
    [SerializeField] private SO_MeleeAttackState stateData;
    public GameObject dung;
    private AttackDetails attackDetails;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animBoolName = "rangedAttack";

        //Update state in entity
        base.OnStateEnter(animator, stateInfo, layerIndex);

        enemy = animator.GetComponentInParent<Enemy>();
        dung = entity.attackPF;

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
    }

    public override void PrepareAttack()
    {
        Debug.Log("prepare");
        base.PrepareAttack();

        //flip bug
        entity.Flip();

        //instantiate the dung game object
        GameObject go = Instantiate(dung, attackPosition.position, Quaternion.identity);
    }

    public override void FinishAttack()
    {
        Debug.Log("finish");

        base.FinishAttack();

        //flip bug back to original position
        entity.Flip();

        //deactivate the dung game object
        //dung.GetComponent<SpriteRenderer>().sprite = null;
    }

    public override void TriggerAttack()
    {
        Debug.Log("trigger");

        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        //@Todo might want to let Dispatcher handle this
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.SendMessage("TakeDamage", enemy.basicAttack);
        }

    }

}
