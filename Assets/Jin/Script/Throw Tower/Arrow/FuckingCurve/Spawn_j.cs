using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_j : MonoBehaviour
{
    public GameObject arrowPrefab;
    public GameObject spawnPos;
    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;

    private bool canSpawn = false;
    private float spawnInterval = 1.5f;
    private float timeSinceLastSpawn = 0f;

    private void Update()
    {
        bool OhMyGod = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(enemyTag))
            {
                OhMyGod = true;
                break;
            }
        }

        if (OhMyGod)
        {
            canSpawn = true;
        }
        else
        {
            canSpawn = false;
        }

        // Check if it's time to spawn a prefab
        if (canSpawn)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                timeSinceLastSpawn = 0f;
                SpawnArrow();
            }
        }
    }

    private void SpawnArrow()
    {
        Instantiate(arrowPrefab, spawnPos.transform.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green; // ±×¸° ½ºÆç¸µ ±î¸Ô¾î¼­ ´çÈ²ÇÒ»· ½´¹ú
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
