using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GameObject selectedGameObject;
    private IMovePosition movePosition;

    private void Awake()
    {
        selectedGameObject = transform.Find("Check").gameObject;
        movePosition = GetComponent<IMovePosition>();
        SetSelectedVisible(false);
    }

    // �̵� ������ �ִ� �Ÿ� ������ ������ ��ġ ��ȯ
    public Vector3 GetPositionWithinMaxDistance(Vector3 targetPosition, float maxDistance)
    {
        Vector3 dir = targetPosition - transform.position;
        if (dir.magnitude <= maxDistance)
        {
            return targetPosition;
        }
        return transform.position + dir.normalized * maxDistance;
    }

    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        movePosition.SetMovePosition(targetPosition);
    }
}
