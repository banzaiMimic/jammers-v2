using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    PLAYER, MITE, ENEMY, BOSS, NULL
}

public abstract class Base : MonoBehaviour
{
    #region Basic stats
    protected float hp = 0f;
    protected float maxHP = 0f;
    protected ElementType type = ElementType.NULL;
    #endregion

    #region Combat stats
    protected float basicAttack = 0f;
    #endregion

    private void Awake()
    {
        InitializeAttributes();
    }

    protected abstract void InitializeAttributes();

    public void TakeDamage(float damageAmount, out bool isDead)
    {
        if (hp == 0) { isDead = true; return; }

        hp = Mathf.Max(hp - damageAmount, 0);

        if (hp != 0) { isDead = false; return; }
        isDead = true;
    }
}
