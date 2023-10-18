using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Boss_Elephant : EnemyBase
{
    [SerializeField] private float specialAttackRange;
    [SerializeField] private float specialAttackDamage;
    
    private int specialAttackCount = 3;

    private int specialAttackAmount;

    protected override void Start()
    {
        base.Start();

        specialAttackAmount = specialAttackCount;
    }

    protected override void AttackUpdate()
    {
        if (specialAttackAmount <= 0)
        {
            // 광역공격
            
            if (attackTimer >= attackRate && !isDie)
            {
                // 공격 로직
            
                anim.SetTrigger("SpecialAttack");
            
                attackTimer = 0f;

                attack = true;
            }

            specialAttackAmount = specialAttackCount;
        
            attackTimer += Time.deltaTime;
            
            return;
        }
        
        // 진화석 공격
        if (isMoveEnd)
        {
            if (attackTimer >= attackRate && !isDie)
            {
                // 공격 로직
            
                anim.SetTrigger("Attack");
            
                attackTimer = 0f;

                attack = true;
            }

            if (attack)
            {
                return;
            }

            specialAttackAmount--;
        
            attackTimer += Time.deltaTime;
            
            return;
        }
        
        if (!isAttacking || targetAlly == null)
        {
            return;
        }
        
        if (Vector2.Distance(transform.position, targetAlly.transform.position ) > attackRange)
        {
            canMove = true;
            isAttacking = false;
        }

        if (attackTimer >= attackRate && !isDie)
        {
            // 공격 로직
            
            anim.SetTrigger("Attack");
            
            attackTimer = 0f;

            attack = true;
        }

        if (attack)
        {
            return;
        }
        
        specialAttackAmount--;
        
        attackTimer += Time.deltaTime;
    }
    
    private void SpecialAttackDamage()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, specialAttackRange);

        foreach (var target in targets)
        {
            if(target.CompareTag("Ally"))
            {
                target.GetComponent<AllyBase>().OnDamage(specialAttackDamage * GameManager.Instance.enemyAttackDamageMultiply, 0);
            }
            else if (target.CompareTag("Evolution Stone"))
            {
                GameManager.Instance.OnEvolutionStoneDamaged(
                    enemyDetailsSo.attackPower * GameManager.Instance.enemyAttackDamageMultiply,
                    enemyDetailsSo.spellPower * GameManager.Instance.enemyAttackDamageMultiply);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, specialAttackRange);
    }
}