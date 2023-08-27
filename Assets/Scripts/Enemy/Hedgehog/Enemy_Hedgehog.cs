using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hedgehog : EnemyBase
{
    [SerializeField] private float scanRange;

    protected override void Update()
    {
        base.Update();
    }

    protected override void AttackUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}
