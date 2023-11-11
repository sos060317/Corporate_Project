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
        [Header("��")]
        public float fireDamage; // ������
        public float fireTime; // ������ �ִ� �ð�
        public int fireCount; // ������ �ִ� Ƚ��

        [Space(10)]
        [Header("��")]
        public float poisonDamage;
        public float poisonTime;
        public int poisonCount;

        [Space(10)]
        [Header("����")]
        public float sllowTime; // ���ο� ���� �ð�
        public float sllowextent; // ���ο� �Ǵ� ���� ex.(0.5f)

        [Space(10)]
        [Header("����")]
        public float timeStopTime; 

    }

}
