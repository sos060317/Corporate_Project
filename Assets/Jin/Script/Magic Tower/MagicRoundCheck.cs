using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRoundCheck : MonoBehaviour
{
    public MagicTowerTemplate MTTemplate;
    public MagicPosCheck UpMTower;
    private int Mlvl;
    public float MgcRound = 5.0f;

    private void Update()
    {
        Mlvl = UpMTower.Magiclevel;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, MgcRound);


        //if (Mlvl == 3)
        //{
        //    MgcRound = 5f;
        //}
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, MgcRound);
    }
}
