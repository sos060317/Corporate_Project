using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArrowTowerTemplate : ScriptableObject
{
    public GameObject ArrowTower;
    public AWeapon[] aweapon;

    [System.Serializable]
    public struct AWeapon
    {
        public int PosLevel;
        public float aDamage;
        public int Acost;
        public float Aradius;
        public int ResellCoset;
        public float AttackSpeed;

        [Space(10)]
        [Header("불")]
        public float fireDamage; // 데미지
        public float fireTime; // 데미지 주는 시간
        public int fireCount; // 데미지 주는 횟수

        [Space(10)]
        [Header("독")]
        public float poisonDamage;
        public float poisonTime;
        public int poisonCount;

        [Space(10)]
        [Header("얼음")]
        public float sllowTime; // 슬로우 지속 시간
        public float sllowextent; // 슬로우 되는 정도 ex.(0.5f)

        [Space(10)]
        [Header("번개")]
        public float timeStopTime; 

    }

}
