using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Armadillo : EnemyBase
{
    protected override void AttackUpdate()
    {
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
        
        attackTimer += Time.deltaTime;
    }
}