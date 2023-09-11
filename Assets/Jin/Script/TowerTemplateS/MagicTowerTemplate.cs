using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MagicTowerTemplate : ScriptableObject
{
    public Weapon[] weapon;

    [System.Serializable]
    public struct Weapon
    {
        public int LeveL;
        public float damage;
        public int Mcost;
        public float Mradius;
    }
}
