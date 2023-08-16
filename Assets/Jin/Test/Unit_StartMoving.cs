using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_StartMoving : MonoBehaviour
{
    public GameObject prefabToMove; // 이동할 Prefab
    public Vector3 moveDirection = Vector3.forward; // 이동할 방향
    public float moveSpeed = 5f; // 이동 속도

    private void Update()
    {
        MovePrefabInstance();
    }

    private void MovePrefabInstance()
    {
        // Prefab이 null이 아닌 경우에만 이동
        if (prefabToMove != null)
        {
            // 이동할 방향과 속도를 곱하여 이동
            prefabToMove.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
