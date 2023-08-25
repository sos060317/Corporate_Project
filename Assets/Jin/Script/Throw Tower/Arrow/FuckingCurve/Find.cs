using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;
    public bool Fuck = false;


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

        Fuck = OhMyGod;
    }
}
