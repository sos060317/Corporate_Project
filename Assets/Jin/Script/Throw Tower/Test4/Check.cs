using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;
    public bool wow = false;
    public Transform summonPoint;
    public GameObject prefabToSummon;
    private Transform target;

    private void Update()
    {
        bool hasEnemy = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(enemyTag))
            {
                target = collider.transform;
                hasEnemy = true;
                break;
            }
        }

        if (!hasEnemy)
        {
            target = null;
        }

        wow = hasEnemy;

        if (wow)
        {
            SpawnAndMovePrefab();
        }
    }

    private void SpawnAndMovePrefab()
    {
        if (prefabToSummon != null && summonPoint != null)
        {
            Instantiate(prefabToSummon, summonPoint.position, Quaternion.identity);
        }
    }}
