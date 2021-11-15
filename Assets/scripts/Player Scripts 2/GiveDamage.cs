using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject hitEffect;
    GameObject enemy;
    Animator anim;
    PlayerMov player;

    public float hitDamage;

    [Header("Attack")]
    bool canAttack = true;
    public bool isSlashing;
    public float canAttackTimeIntervel;
    float canAttackTimer;

    bool enemyInRange;
    bool hitOneTime;

    float timer = 0.1f;

    private void Start()
    {
        canAttackTimer = canAttackTimeIntervel;
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMov>();
        hitOneTime = true;
    }

    private void Update()
    {
        isSlashing = !anim.GetCurrentAnimatorStateInfo(0).IsName("IdleSlash");

        if (!isSlashing)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                parentObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                parentObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }

        Attack();        
    }

    private void Attack()
    {
        if (canAttack && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.J)))
        {
            GetComponent<Animator>().SetTrigger("slash");
            canAttack = false;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
        {
            if (enemyInRange && hitOneTime)
            {
                enemy.GetComponent<Enemy_TakeDamage>().TakeDamage(hitDamage);
                GameObject effect = Instantiate(hitEffect, enemy.transform.position, Quaternion.identity);

                player.Recoil();

                Destroy(effect, 1f);
                hitOneTime = false;
            }
        }
        DisableCanAttack();

        DisableEnemyInRange();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = true;
            enemy = collision.gameObject;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = null;
        }
    }

    private void DisableCanAttack()
    {
        if (!canAttack)
        {
            canAttackTimer -= Time.deltaTime;
        }
        if (canAttackTimer < 0)
        {
            canAttack = true;
            hitOneTime = true;
            canAttackTimer = canAttackTimeIntervel;
        }
    }

    private void DisableEnemyInRange()
    {
        if (enemyInRange)
        {
            timer -= Time.deltaTime;
        }
        if (timer < 0)
        {
            enemyInRange = false;
            timer = 0.5f;
        }
    }
}
