using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elite_Wolf : EnemyBase
{
    [SerializeField] private float bloodSuckingRate;

    protected override void AttackUpdate()
    {
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

            float absorptionHealth = enemyDetailsSo.attackPower * (bloodSuckingRate * 0.01f);

            Debug.Log(absorptionHealth);
            
            curHealth = Mathf.Min(curHealth + absorptionHealth, maxHealth);
            
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
