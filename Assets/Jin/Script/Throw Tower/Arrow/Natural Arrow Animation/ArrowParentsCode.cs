using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowParentsCode : MonoBehaviour
{
    public GameObject arrow;

    void Update()
    {
        // arrow ������Ʈ�� ��Ȱ��ȭ�Ǹ� �� ��ũ��Ʈ�� �� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
        if (arrow != null && !arrow.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
