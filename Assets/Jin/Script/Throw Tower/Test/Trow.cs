using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trow : MonoBehaviour
{
    //public GameObject arrowPrefab;
    //public Transform arrowSpawnPoint;
    //public float shootingForce = 10f;
    //public float detectionRange = 5f;
    //public float attackDelay = 1f;

    //private Transform targetEnemy;
    //private bool isAttacking;
    //private Vector3 attackTargetPosition;
    //private float lastAttackTime;

    //private void Update()
    //{
    //    if (!isAttacking && Time.time - lastAttackTime >= attackDelay)
    //    {
    //        FindAndAttackEnemy();
    //    }
    //}

    //void FindAndAttackEnemy()
    //{
    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
    //    Transform closestEnemy = null;
    //    float closestDistance = Mathf.Infinity;

    //    foreach (Collider2D collider in colliders)
    //    {
    //        if (collider.CompareTag("Enemy"))
    //        {
    //            float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);
    //            if (distanceToEnemy < closestDistance)
    //            {
    //                closestDistance = distanceToEnemy;
    //                closestEnemy = collider.transform;
    //            }
    //        }
    //    }

    //    if (closestEnemy != null)
    //    {
    //        targetEnemy = closestEnemy;
    //        attackTargetPosition = targetEnemy.position;
    //        lastAttackTime = Time.time;
    //        isAttacking = true;
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    if (isAttacking)
    //    {
    //        Vector3 direction = (attackTargetPosition - arrowSpawnPoint.position).normalized;

    //        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
    //        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
    //        arrowRb.velocity = direction * shootingForce;

    //        ArrowScript arrowScript = arrow.GetComponent<ArrowScript>();
    //        arrowScript.SetTarget(targetEnemy, () =>
    //        {
    //            isAttacking = false;
    //            targetEnemy = null;
    //        });

    //        isAttacking = false; // Set isAttacking to false immediately after firing the arrow
    //    }
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, detectionRange);
    //}



    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float shootingForce = 10f;
    public float detectionRange = 5f;
    public float attackDelay = 1f;

    private Transform targetEnemy;
    private bool isAttacking;
    private Vector3 attackTargetPosition;
    private float lastAttackTime;

    private void Update()
    {
        if (!isAttacking && Time.time - lastAttackTime >= attackDelay)
        {
            FindAndAttackEnemy();
        }
    }

    void FindAndAttackEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = collider.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            targetEnemy = closestEnemy;
            attackTargetPosition = targetEnemy.position;
            lastAttackTime = Time.time;
            isAttacking = true;

            SpawnArrow(); // Only spawn the arrow without applying force
        }
    }

    void SpawnArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        ArrowScript arrowScript = arrow.GetComponent<ArrowScript>();
        arrowScript.SetTarget(targetEnemy, arrowSpawnPoint.position, () =>
        {
            isAttacking = false;
            targetEnemy = null;
        });
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
