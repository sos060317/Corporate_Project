using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    public GameObject prefabToSpawn; // ��ȯ�� Prefab
    public float spawnInterval = 2f; // ��ȯ ���� (��)

    private void Start()
    {
        // spawnInterval���� Spawn �Լ��� ȣ���ϵ��� ����
        InvokeRepeating("Spawn", 0f, spawnInterval);
    }

    private void Spawn()
    {
        // Prefab�� ���� ��ũ��Ʈ�� ���� ��ġ�� ��ȯ
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
}
