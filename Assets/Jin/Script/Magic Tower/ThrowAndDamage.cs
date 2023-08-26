using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAndDamage : MonoBehaviour
{
    public string enemyTag = "FlowingPos";
    public float speed = 10f;
    public float initialDelay = 0.2f;

    private Transform target;
    private Vector3 targetPosition;
    private bool isFollowing = true;

    private void Start()
    {
        Invoke("StopFollowing", initialDelay);
    }

    private void StopFollowing()
    {
        isFollowing = false;

        GameObject[] flowingObjects = GameObject.FindGameObjectsWithTag(enemyTag);
        if (flowingObjects.Length > 0)
        {
            target = flowingObjects[0].transform;
            targetPosition = target.position;
            isFollowing = true;
        }
        else
        {
            // FlowingPos1 �±׸� ���� ������Ʈ�� ���� ��� ó���� ���� �߰�
            Destroy(gameObject); // ���÷� ��ũ��Ʈ�� ����� ������Ʈ�� �ı�
        }
    }

    private void Update()
    {
        if (isFollowing && target != null)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
