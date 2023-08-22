using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBase : MonoBehaviour
{
    [SerializeField] private LayerMask scanLayer;
    [SerializeField] private AllyDetailsSO allyDetailsSo;
    
    [SerializeField] private float scanRange;

    private float maxHealth;
    private float curHealth;
    private float moveSpeed;
    private float attackRange;

    private bool isTargeting;
    private bool canMove;
    private bool isAttacking;
    private bool isDie;

    private SpriteRenderer sr;
    
    private GameObject targetObj;

    private void Start()
    {
        // 컴포넌트 할당
        sr = GetComponent<SpriteRenderer>();
        
        // 변수 초기화
        isTargeting = false;
        canMove = true;
        isAttacking = false;
        isDie = false;

        moveSpeed = allyDetailsSo.allyBaseMoveSpeed;
        attackRange = allyDetailsSo.allyBaseAttackRange;

        maxHealth = allyDetailsSo.allyBaseHealth;
        curHealth = maxHealth;
    }

    private void Update()
    {
        MoveUpdate();
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
            Vector3 dir = targetObj.transform.position - transform.position;
            transform.position += dir.normalized * (moveSpeed * Time.deltaTime);
            
            if (dir.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

            if (Vector2.Distance(transform.position, targetObj.transform.position) <= attackRange)
            {
                canMove = false;
                isAttacking = true;
            }

            return;
        }
    }

    private void CheckTarget()
    {
        if (isTargeting)
        {
            return;
        }
        
        Collider2D target;
        
        target = Physics2D.OverlapCircle(transform.position, scanRange, scanLayer);

        if (target != null)
        {
            targetObj = target.gameObject;
            isTargeting = true;
        }
    }

    public void OnDamage(float damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            // 죽는 로직

            gameObject.SetActive(false);
            isDie = true;
        }
    }
    
    public bool CheckAllyIsDie()
    {
        return isDie;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}