using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroRoundCheck : MonoBehaviour
{
    public ArrowTowerTemplate ATTemplate;
    public UpgradeArrowTower UpATower;
    private int Alvl;
    public float AroRound = 7.0f;

    private void Update()
    {
        Alvl = UpATower.Arrowlevel;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, AroRound);


        if (Alvl == 3)
        {
            AroRound = 9f;
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AroRound);
    }
}
