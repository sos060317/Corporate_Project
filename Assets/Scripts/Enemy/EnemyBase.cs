using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private bool canMove = true;
    
    private Vector2[] movePoints;
    private Vector3 moveOffset;

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
    }

    public void InitEnemy(Vector2[] movePoints)
    {
        this.movePoints = movePoints;
        
        moveOffset = new Vector3(0, Random.Range(-1f, 1f), 0);

        canMove = true;
    }
}