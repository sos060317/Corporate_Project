using UnityEngine;

public class Enemy_Dog : EnemyBase
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

    public override void OnDamage(float attackPower, float spellPower)
    {
        curHealth -= (attackPower - (attackPower * (enemyDetailsSo.defense * 0.01f))) + (spellPower - (spellPower * (enemyDetailsSo.magicResistance * 0.01f)));

        if (curHealth <= 0)
        {
            // 죽는 로직

            isDie = true;
            
            if (targetAlly != null)
            {
                targetAlly.DeleteTarget();
            }
            
            anim.SetTrigger("Die");
            return;
        }
        
        StartCoroutine(HitRoutine());
    }
}
