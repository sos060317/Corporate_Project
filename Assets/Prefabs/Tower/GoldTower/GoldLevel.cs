using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GoldLevel : ScriptableObject
{
    public GameObject GoldTower;
    public GoldTowerLevel[] goldTowerLevel;

    [System.Serializable]
    public struct GoldTowerLevel
    {
        public float freshnessTime; // 바나나 신선도 시간
        public float spawnTime; // 소환 속도
        public float amountGold; // 골드량

        public float Cost;
        public float ResellGold;
    }
}
