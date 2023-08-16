using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_StartMoving : MonoBehaviour
{
    public GameObject prefabToMove; // �̵��� Prefab
    public Vector3 moveDirection = Vector3.forward; // �̵��� ����
    public float moveSpeed = 5f; // �̵� �ӵ�

    private void Update()
    {
        MovePrefabInstance();
    }

    private void MovePrefabInstance()
    {
        // Prefab�� null�� �ƴ� ��쿡�� �̵�
        if (prefabToMove != null)
        {
            // �̵��� ����� �ӵ��� ���Ͽ� �̵�
            prefabToMove.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
