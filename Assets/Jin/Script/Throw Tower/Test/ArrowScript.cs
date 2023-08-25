using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    //public float damage = 10f; // µ¥¹ÌÁö

    //private Transform target;
    //private System.Action onHitCallback;

    //public void SetTarget(Transform newTarget, System.Action callback)
    //{
    //    target = newTarget;
    //    onHitCallback = callback;
    //}

    //private void Update()
    //{
    //    if (target == null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    Vector3 direction = (target.position - transform.position).normalized;
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    //    transform.Translate(direction * Time.deltaTime, Space.World);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy") && collision.transform == target)
    //    {
    //        //EnemyScript enemy = collision.GetComponent<EnemyScript>();
    //        //if (enemy != null)
    //        //{
    //        //    enemy.TakeDamage(damage);
    //        //}

    //        if (onHitCallback != null)
    //        {
    //            onHitCallback.Invoke();
    //        }

    //        Destroy(gameObject);
    //    }
    //}




    public float damage = 10f;
    public float speed = 5f;
    public float bezierHeight = 2f;

    private Transform target;
    private Vector3 spawnPosition;
    private Vector3[] bezierPoints;
    private int currentPointIndex;
    private System.Action onHitCallback;

    public void SetTarget(Transform newTarget, Vector3 spawnPos, System.Action callback)
    {
        target = newTarget;
        spawnPosition = spawnPos;
        onHitCallback = callback;

        Vector3 middlePoint = (target.position + spawnPosition) / 2f;
        Vector3 offset = middlePoint - spawnPosition;
        Vector3 controlPoint = middlePoint + Vector3.up * bezierHeight;

        bezierPoints = new Vector3[] { spawnPosition, controlPoint, target.position };
        currentPointIndex = 0;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (currentPointIndex < bezierPoints.Length)
        {
            Vector3 nextPoint = bezierPoints[currentPointIndex];
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, step);

            if (Vector3.Distance(transform.position, nextPoint) <= 0.01f)
            {
                currentPointIndex++;
                if (currentPointIndex == bezierPoints.Length)
                {
                    if (onHitCallback != null)
                    {
                        onHitCallback.Invoke();
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.transform == target)
        {
            if (onHitCallback != null)
            {
                onHitCallback.Invoke();
            }

            Destroy(gameObject);
        }
    }
}
