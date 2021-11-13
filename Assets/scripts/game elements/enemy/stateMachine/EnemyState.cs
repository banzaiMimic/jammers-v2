using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : StateMachineBehaviour
{
    protected Entity_ entity;
    protected float startTime;
    protected string animBoolName;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        entity = animator.gameObject.GetComponent<Entity_>();
        entity.ChangeCurrentState(this);

        //State Enter
        startTime = Time.time;
        entity.animator.SetBool(animBoolName, true);
        DoChecks();
    }

    protected void ChangeState(string from, string to)
    {
        entity.animator.SetBool(from, false);
        entity.animator.SetBool(to, true);
    }

    public virtual void DoChecks() { }

    public virtual void OnFixedUpdate()
    {
        DoChecks();
    }
}
