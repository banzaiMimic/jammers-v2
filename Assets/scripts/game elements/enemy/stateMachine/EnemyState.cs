using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : StateMachineBehaviour
{
    protected Entity_ entity;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        entity = animator.gameObject.GetComponent<Entity_>();
        entity.ChangeCurrentState(this);
    }

    public abstract void OnFixedUpdate();
}
