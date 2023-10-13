using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BombTowerTemplate : ScriptableObject
{
    public BWeapon[] Bweapon;

    [System.Serializable]
    public struct BWeapon
    {
        public int Bcost; // ���
        public float damage;
        public float round;
        public float shotSpeed;
    }
}
