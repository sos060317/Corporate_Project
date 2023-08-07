using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadCreator))]
[RequireComponent(typeof(PathCreator))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveDetailsSO wave;

    private int curWaveIndex;
    
    private PathCreator path;

    private Vector2 spawnPos;

    private WaitForSeconds enemySpawnTimer;

    private void Start()
    {
        // Load Component
        path = GetComponent<PathCreator>();
        
        // Variable initialize
        spawnPos = path.path[0];
        curWaveIndex = 0;
        
        // Wave Start
        StartCoroutine(WaveRoutine());
    }
    
    private IEnumerator WaveRoutine()
    {
        WaveDetailsSO.WaveData curWaveData;
        
        for (int waveDataIndex = 0; waveDataIndex < wave.waves[curWaveIndex].waveDatas.Length; waveDataIndex++)
        {
            curWaveData = wave.waves[curWaveIndex].waveDatas[waveDataIndex];
            
            enemySpawnTimer = new WaitForSeconds(curWaveData.enemySpawnInterval);

            for (int enemySpawnCount = 0;
                 enemySpawnCount < curWaveData.enemySpawnCount;
                 enemySpawnCount++)
            {
                var enemy = Instantiate(curWaveData.enemyType.enemyPrefab, spawnPos, Quaternion.identity);
                
                enemy.GetComponent<EnemyBase>().InitEnemy(path.path.CalculateEvenlySpacedPoints(0.1f), curWaveData.enemyType);

                yield return enemySpawnTimer;
            }

            yield return new WaitForSeconds(curWaveData.nextEnemyDelay);
        }
    }
}
