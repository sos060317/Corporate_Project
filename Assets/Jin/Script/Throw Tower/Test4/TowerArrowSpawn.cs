using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArrowSpawn : MonoBehaviour
{
    public Check tower;
    public GameObject prefabToSummon;

    private void Update()
    {
        if (tower != null)
        {
            if (tower.wow)
            {
                SpawnPrefab();
            }
        }
    }

    private void SpawnPrefab()
    {
        Vector3 spawnPosition = tower.summonPoint.position;

        Instantiate(prefabToSummon, spawnPosition, Quaternion.identity);
    }
}
