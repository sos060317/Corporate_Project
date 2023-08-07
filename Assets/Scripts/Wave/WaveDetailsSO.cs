using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveDetails_", menuName = "Scriptable Objects/Wave/WaveDetails")]
public class WaveDetailsSO : ScriptableObject
{
    public Wave[] waves;
    
    [System.Serializable]
    public struct Wave
    {
        public WaveData[] waveDatas;
    }
    
    [System.Serializable]
    public struct WaveData
    {
        [Tooltip("스폰 할 적의 데이터")]
        public EnemyDetailsSO enemyType;
        
        [Tooltip("적을 소환할 수")]
        public int enemySpawnCount;

        [Tooltip("적 스폰 간격")]
        public float enemySpawnInterval;

        [Tooltip("다음 적 스폰까지의 딜레이")]
        public float nextEnemyDelay;
    }
}