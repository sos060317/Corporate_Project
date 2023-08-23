using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBase : MonoBehaviour
{
    [SerializeField] private LayerMask scanLayer;
    [SerializeField] private AllyDetailsSO allyDetailsSo;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material defaultMaterial;
    
    [SerializeField] private float scanRange;

    [HideInInspector] public bool targeting;

    private float maxHealth;
    private float curHealth;
    private float moveSpeed;
    private float attackRange;
    private float attactRate;
    private float attactTimer;
    private float attackDamage;

    private bool isTargeting;
    private bool canMove;
    private bool isAttacking;
    private bool isDie;
    private bool isRun;
    private bool attack;

    private SpriteRenderer sr;
    private Animator anim;
    private WaitForSeconds hitDelay;
    
    private EnemyBase targetEnemy;

    private void OnEnable()
    {
        attactTimer = 0f;
        isDie = false;
    }

    private void Start()
    {
        // 컴포넌트 할당
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        // 변수 초기화
        isTargeting = false;
        canMove = true;
        isAttacking = false;
        isDie = false;
        isRun = false;
        hitDelay = new WaitForSeconds(0.1f);

        moveSpeed = allyDetailsSo.allyBaseMoveSpeed;
        attackRange = allyDetailsSo.allyBaseAttackRange;
        attactRate = allyDetailsSo.allyBaseAttackDelay;
        attackDamage = allyDetailsSo.allyBaseAttackDamage;

        maxHealth = allyDetailsSo.allyBaseHealth;
        curHealth = maxHealth;
    }

    private void Update()
    {
        MoveUpdate();
        AttackUpdate();
        AnimationUpdate();
    }

    private void FixedUpdate()
    {
        CheckTarget();
    }
    
    private void MoveUpdate()
    {
        if (!canMove)
        {
            return;
        }
        
        if (isTargeting)
        {
            var dir = targetEnemy.transform.position - transform.position;
            transform.position += dir.normalized * (moveSpeed * Time.deltaTime);
            
            if (dir.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

            isRun = true;

            if (Vector2.Distance(transform.position, targetEnemy.transform.position) <= attackRange)
            {
                canMove = false;
                isAttacking = true;
                isRun = false;
            }
        }
    }
    
    private void AttackUpdate()
    {
        if (!isAttacking || targetEnemy == null)
        {
            return;
        }

        if (attack)
        {
            return;
        }

        if (attactTimer >= attactRate && !isDie)
        {
            // 공격 로직
            
            anim.SetTrigger("Attack");
            
            attactTimer = 0f;

            attack = true;
        }

        attactTimer += Time.deltaTime;
    }

    private void AnimationUpdate()
    {
        anim.SetBool("IsRun", isRun);
    }

    private void CheckTarget()
    {
        if (isTargeting)
        {
            return;
        }
        
        var target = Physics2D.OverlapCircle(transform.position, scanRange, scanLayer);

        if (target != null)
        {
            if (!target.GetComponent<EnemyBase>().Targeting)
            {
                targetEnemy = target.gameObject.GetComponent<EnemyBase>();
                targetEnemy.SetTarget(this);
                targetEnemy.Targeting = true;
                isTargeting = true;
            }
        }
    }
    
    // 애니메이션 이벤트에서 사용할 함수
    private void AttackEnd()
    {
        attack = false;
    }
    
    // 애니메이션 이벤트에서 사용할 함수.
    private void AttackDamage()
    {
        if (targetEnemy == null)
        {
            return;
        }
        
        targetEnemy.OnDamage(attackDamage);
    }
    
    private IEnumerator HitRoutine()
    {
        sr.material = hitMaterial;

        yield return hitDelay;

        sr.material = defaultMaterial;
    }
    
    public void DeleteTarget()
    {
        isTargeting = false;
        canMove = true;
        isAttacking = false;
        attack = false;
        targetEnemy = null;
    }

    public void OnDamage(float damage)
    {
        curHealth -= damage;
        
        if (curHealth <= 0)
        {
            // 죽는 로직

            isDie = true;
            targetEnemy.DeleteTarget();
            anim.SetTrigger("Die");
            return;
        }
        
        StartCoroutine(HitRoutine());
    }
    
    // 애니메이션 이벤트에 사용할 함수.
    private void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}