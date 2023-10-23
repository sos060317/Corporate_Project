using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroRoundCheck : MonoBehaviour
{
    public float AroRound = 7.0f;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, AroRound);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AroRound);
    }
}
