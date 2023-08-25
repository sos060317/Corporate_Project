using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    public GameObject prefabToSpawn; // 소환할 Prefab
    public float spawnInterval = 2f; // 소환 간격 (초)

    private void Start()
    {
        // spawnInterval마다 Spawn 함수를 호출하도록 설정
        InvokeRepeating("Spawn", 0f, spawnInterval);
    }

    private void Spawn()
    {
        // Prefab을 현재 스크립트가 속한 위치에 소환
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
}
