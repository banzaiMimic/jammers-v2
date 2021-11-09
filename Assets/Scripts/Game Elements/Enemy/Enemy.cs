using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Base
{
    protected override void InitializeAttributes()
    {
        type = ElementType.ENEMY;
        hp = 3f;
        basicAttack = 1f;
    }
}
