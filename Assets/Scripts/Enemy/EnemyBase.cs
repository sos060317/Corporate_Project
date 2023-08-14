using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float scanRange;
    [SerializeField] private LayerMask scanLayer;
    
    private int movePosIndex;
    
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

    private void Update()
    {
        MoveUpdate();
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
            transform.position += dir.normalized * (enemyDetailsSo.enemyBaseMoveSpeed * Time.deltaTime);
            
            if (dir.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

            if (Vector2.Distance(transform.position, targetObj.transform.position) <= 1f)
            {
                canMove = false;
                isAttacking = true;
            }
            
            return;
        }
        
        Vector3 nextPos = (Vector3)movePoints[movePosIndex] - transform.position + (Vector3)moveOffset;
        transform.position += nextPos.normalized * (enemyDetailsSo.enemyBaseMoveSpeed * Time.deltaTime);
        
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

    public void InitEnemy(Vector2[] movePoints, EnemyDetailsSO enemyDetailsSo)
    {
        this.movePoints = movePoints;
        this.enemyDetailsSo = enemyDetailsSo;

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