using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffDetails_", menuName = "Scriptable Objects/Buff/BuffDetails")]
public class BuffDetailsSO : ScriptableObject
{
    public BuffDataInfo[] buffData;
    
    [System.Serializable]
    public struct BuffDataInfo
    {
        public float buffForce;
        public int needGold;
        [TextArea] public string buffExplanation;
    }
}