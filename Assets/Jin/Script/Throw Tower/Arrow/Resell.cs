using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resell : MonoBehaviour
{
    private GameManager playerGold;
    public ArrowTowerTemplate arrowTemplate;
    private int level;
    private int resellCost;

    private void Update()
    {
        resellCost = arrowTemplate.aweapon[level].ResellCoset;  //ResellCost¿Œµ• ∞Ìƒ°±‚ ±Õ¬˙¿Ω
    }

    public void ResellCoinPrice()
    {
        Destroy(gameObject);

        playerGold.currentGold += resellCost;
    }


}
