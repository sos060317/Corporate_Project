using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private LayerMask scanLayer;
    
    private int movePosIndex;

    private float attackRate;
    private float attackTimer;
    private float scanRange;
    private float attackRange;
    private float moveSpeed;
    
    private bool canMove = true;
    private bool isTargeting = false;
    private bool isAttacking = false;
    
    private Vector2[] movePoints;
    private Vector2 moveOffset;

    private SpriteRenderer sr;
    private EnemyDetailsSO enemyDetailsSo;

    private GameObject targetObj;

    private void Awake()
    {
        // Load Component
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        attackTimer = 0f;
    }

    private void Update()
    {
        MoveUpdate();
        AttackUpdate();
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
        
        // 주변에 타겟이 있을때
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
        
        Vector3 nextPos = (Vector3)movePoints[movePosIndex] - transform.position + (Vector3)moveOffset;
        transform.position += nextPos.normalized * (moveSpeed * Time.deltaTime);
        
        if (nextPos.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        if (Vector2.Distance(transform.position - (Vector3)(moveOffset), movePoints[movePosIndex]) <= 0.01f)
        {
            movePosIndex++;
        }

        if (movePosIndex >= movePoints.Length)
        {
            //GameManager.Instance.defianceLife--;
            Destroy(gameObject);
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
            if(!target.GetComponent<TargetTest>().targeting)
            {
                target.GetComponent<TargetTest>().targeting = true;
                targetObj = target.gameObject;
                isTargeting = true;
            }
        }
    }
    private void AttackUpdate()
    {
        if (!isAttacking)
        {
            return;
        }

        if (attackTimer >= attackRate)
        {
            // 공격 로직
            attackTimer = 0f;
        }

        attackTimer += Time.deltaTime;
    }

    public void InitEnemy(Vector2[] movePoints, EnemyDetailsSO enemyDetailsSo)
    {
        this.movePoints = movePoints;
        this.enemyDetailsSo = enemyDetailsSo;

        moveSpeed = this.enemyDetailsSo.enemyBaseMoveSpeed;
        scanRange = this.enemyDetailsSo.enemyBaseScanRange;
        attackRate = this.enemyDetailsSo.enemyBaseAttackRate;
        attackRange = this.enemyDetailsSo.enemyBaseAttackRange;

        movePosIndex = 0;
        
        moveOffset = new Vector3(0, Random.Range(-1f, 1f));

        transform.position = movePoints[movePosIndex] + moveOffset;

        canMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}