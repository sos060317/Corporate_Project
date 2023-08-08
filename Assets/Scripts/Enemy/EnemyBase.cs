using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    private int movePosIndex;
    
    private bool canMove = true;
    
    private Vector2[] movePoints;
    private Vector2 moveOffset;

    private SpriteRenderer sr;
    private EnemyDetailsSO enemyDetailsSo;

    private void Awake()
    {
        // Load Component
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MoveUpdate();
    }
    
    private void MoveUpdate()
    {
        if (!canMove)
        {
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

    public void InitEnemy(Vector2[] movePoints, EnemyDetailsSO enemyDetailsSo)
    {
        this.movePoints = movePoints;
        this.enemyDetailsSo = enemyDetailsSo;

        movePosIndex = 0;
        
        moveOffset = new Vector3(0, Random.Range(-1f, 1f));

        transform.position = movePoints[movePosIndex] + moveOffset;

        canMove = true;
    }
}