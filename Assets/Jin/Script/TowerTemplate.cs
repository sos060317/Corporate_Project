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
        //public GameObject sprite;         // �Ƹ��� ������Ʈ�� ���� ������?
        public float Unitdamage;            // Unit ������
        public float UnitHp;                // UnitHp
        public float SpawnTime;             // ���� �ð�
        public int MaxUnit;                 // �Ƹ��� ����?
        public int cost;                    // ���
    }

}
