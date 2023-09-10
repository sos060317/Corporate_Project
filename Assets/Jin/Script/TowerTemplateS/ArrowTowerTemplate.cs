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
    }

}
