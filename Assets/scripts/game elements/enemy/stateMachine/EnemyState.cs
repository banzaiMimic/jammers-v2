using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : StateMachineBehaviour
{
    public abstract void OnFixedUpdate();
}
