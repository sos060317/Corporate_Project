using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MagicTowerTemplate : ScriptableObject
{
    public MWeapon[] mweapon;

    [System.Serializable]
    public struct MWeapon
    {
        public int LeveL;
        public float damage;
        public int Mcost;
        public float Mradius;
        public int ResellCost;
    }
}
