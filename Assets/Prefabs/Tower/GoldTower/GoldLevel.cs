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
        public float freshnessTime; // �ٳ��� �ż��� �ð�
        public float spawnTime; // ��ȯ �ӵ�
        public float amountGold; // ��差

        public float Cost;
        public float ResellGold;
    }
}
