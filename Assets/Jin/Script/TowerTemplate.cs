using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject
{
    public GameObject UnitTowerPrefab;
    public Weapon[] weapon;

    [System.Serializable]
    public struct Weapon
    {
        //public GameObject sprite;         // 아마도 오브젝트로 넣지 않을까?
        public float Unitdamage;            // Unit 데미지
        public float UnitHp;                // UnitHp
        public float SpawnTime;             // 스폰 시간
        public int MaxUnit;                 // 아마도 유닛?
        public int cost;                    // 비용
    }

}
