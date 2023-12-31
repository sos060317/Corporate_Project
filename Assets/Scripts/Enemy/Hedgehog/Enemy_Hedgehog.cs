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
                        targetAlly.EnemyUnTargetingEvent -= DeleteTarget;
                    }
                    
                    targetAlly = target.gameObject.GetComponent<AllyBase>();
                    
                    target.gameObject.GetComponent<AllyBase>().EnemyUnTargetingEvent += DeleteTarget;
                    
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
        #region 진화석 공격 로직

        if (isMoveEnd)
        {
            if (attackTimer >= attackRate && !isDie)
            {
                // 공격 로직
            
                anim.SetTrigger("MeleeAttack");
            
                attackTimer = 0f;

                attack = true;
            }

            if (attack)
            {
                return;
            }
        
            attackTimer += Time.deltaTime;

            return;
        }
        
        #endregion
        
        if (!isAttacking || targetAlly == null)
        {
            return;
        }
        
        if (Vector2.Distance(transform.position, targetAlly.transform.position ) >= enemyDetailsSo.enemyBaseAttackRange + 1)
        {
            canMove = true;
            isAttacking = false;
            isTargeting = false;
            targetAlly = null;
        }

        #region 일반 공격 로직

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
                
                Debug.Log("원거리 공격");
            }
            
            attackTimer = 0f;

            attack = true;
        }
        
        #endregion

        if (attack)
        {
            return;
        }
        
        attackTimer += Time.deltaTime;
        
        Debug.Log(attackTimer);
    }

    private void ShotThorn()
    {
        if (targetAlly == null)
        {
            return;
        }
        
        Instantiate(thornPrefab, transform.position, Quaternion.identity).StartShot(transform.position,
            targetAlly.transform.position, targetAlly, enemyDetailsSo.attackPower);
    }

    public override void OnDamage(float attackPower, float spellPower)
    {
        if (isDie)
        {
            return;
        }
        
        curHealth = 
            Mathf.Max(
                curHealth - ((attackPower - (attackPower * (defense * 0.01f))) + 
                             (spellPower - (spellPower * (magicResistance * 0.01f)))), 0);

        if (curHealth <= 0)
        {
            // 죽는 로직

            isDie = true;
            
            GameManager.Instance.GetGold(enemyDetailsSo.coins);
            
            transform.GetComponent<Collider2D>().enabled = false;
            
            if (targetAlly != null)
            {
                targetAlly.DeleteTarget();
                targetAlly.targeting = false; 
            }
            
            SoundManager.Instance.PlaySound(dieSound);
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            
            anim.SetTrigger("Die");
            return;
        }
        
        StartCoroutine(HitRoutine());
    }

    public override void SetTarget(AllyBase ally)
    {
        if (targetAlly != null)
        {
            targetAlly.EnemyUnTargetingEvent -= DeleteTarget;
            targetAlly.targeting = false;
        }
        
        targetAlly = ally;

        ally.EnemyUnTargetingEvent += DeleteTarget;
        
        isTargeting = true;
        Targeting = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, enemyDetailsSo.enemyBaseAttackRange);
    }
}
