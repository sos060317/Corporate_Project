using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroRoundCheck : MonoBehaviour
{
    public ArrowTowerTemplate ATTemplate;
    public UpgradeArrowTower UpATower;
    private int Alvl;
    public float AroRound;

    private void Update()
    {
        Alvl = UpATower.Arrowlevel;
        AroRound = ATTemplate.aweapon[Alvl].Aradius;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, AroRound);

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AroRound);
    }
}
