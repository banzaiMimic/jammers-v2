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
    public float basicAttack { get; protected set; } = 0f;
    #endregion

    protected void Awake()
    {
        InitializeAttributes();
    }

    protected abstract void InitializeAttributes();

    public virtual bool TakeDamage(float damageAmount)
    {
        if (hp == 0) { return true; }

        hp = Mathf.Max(hp - damageAmount, 0);

        if (hp != 0) { return false; }
        return true;
    }
}
