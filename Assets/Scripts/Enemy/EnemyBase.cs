using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    private int movePosIndex;
    
    private bool canMove = true;
    
    private Vector2[] movePoints;
    private Vector3 moveOffset;

    private SpriteRenderer sr;

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
        
        Vector3 nextPos = (Vector3)movePoints[movePosIndex] - transform.position + moveOffset;
        transform.position += nextPos.normalized * (moveSpeed * Time.deltaTime);
        
        if (nextPos.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        if (Vector2.Distance(transform.position - moveOffset, movePoints[movePosIndex]) <= 0.01f)
        {
            movePosIndex++;
        }

        if (movePosIndex >= movePoints.Length)
        {
            Destroy(gameObject);
        }
    }

    public void InitEnemy(Vector2[] movePoints)
    {
        this.movePoints = movePoints;
        
        moveOffset = new Vector3(0, Random.Range(-1f, 1f), 0);

        canMove = true;
    }
}