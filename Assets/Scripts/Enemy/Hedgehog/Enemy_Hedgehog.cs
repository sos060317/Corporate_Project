using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hedgehog : EnemyBase
{
    [SerializeField] private float scanRange;
    [SerializeField] private LayerMask scanLayer;
    [SerializeField] private HedgehogThorn thornPrefab;
    
    protected override void Update()
    {
        if (isDie)
        {
            return;
        }
        
        base.Update();
        
        CheckTarget();
    }
    
    private void CheckTarget()
    {
        if (isTargeting)
        {
            return;
        }
        
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, enemyDetailsSo.enemyBaseAttackRange, scanLayer);

        if (targets != null)
        {
            foreach (var target in targets)
            {
                if (!target.GetComponent<AllyBase>().targeting)
                {
                    if (targetAlly != null)
                    {
                        targetAlly.DieEvent -= DeleteTarget;
                    }
                    
                    targetAlly = target.gameObject.GetComponent<AllyBase>();
                    
                    target.gameObject.GetComponent<AllyBase>().DieEvent += DeleteTarget;
                    
                    isAttacking = true;
                    isTargeting = true;
                    canMove = false;
                    targetAlly.targeting = true;

                    break;
                }
            }
        }
    }

    protected override void AttackUpdate()
    {
        if (!isAttacking || targetAlly == null)
        {
            return;
        }
        
        if (Vector2.Distance(transform.position, targetAlly.transform.position ) >= enemyDetailsSo.enemyBaseAttackRange + 1)
        {
            Debug.Log("a");
            canMove = true;
            isAttacking = false;
            isTargeting = false;
        }

        if (attackTimer >= attackRate && !isDie)
        {
            // 공격 로직

            if (Vector2.Distance(transform.position, targetAlly.transform.position) <=
                0.4f)
            {
                // 근거리 공격
                
                anim.SetTrigger("MeleeAttack");

            }
            else
            {
                // 원거리 공격
                
                anim.SetTrigger("Attack");

                var thorn = Instantiate(thornPrefab, transform.position, Quaternion.identity);

                thorn.StartShot(transform.position, targetAlly.transform.position, targetAlly, enemyDetailsSo.attackPower);
            }
            
            attackTimer = 0f;

            attack = true;
        }

        if (attack)
        {
            return;
        }
        
        attackTimer += Time.deltaTime;
    }

    public override void OnDamage(float attackPower, float spellPower)
    {
        curHealth -= (attackPower - (attackPower * (enemyDetailsSo.defense * 0.01f))) + (spellPower - (spellPower * (enemyDetailsSo.magicResistance * 0.01f)));

        if (curHealth <= 0)
        {
            // 죽는 로직

            isDie = true;
            
            transform.GetComponent<Collider2D>().enabled = false;
            
            if (targetAlly != null)
            {
                targetAlly.DeleteTarget();
                targetAlly.targeting = false;
            }
            
            anim.SetTrigger("Die");
            return;
        }
        
        StartCoroutine(HitRoutine());
    }

    public override void SetTarget(AllyBase ally)
    {
        if (targetAlly != null)
        {
            targetAlly.DieEvent -= DeleteTarget;
            targetAlly.targeting = false;
        }
        
        targetAlly = ally;

        ally.DieEvent += DeleteTarget;
        
        isTargeting = true;
        Targeting = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, enemyDetailsSo.enemyBaseAttackRange);
    }
}
