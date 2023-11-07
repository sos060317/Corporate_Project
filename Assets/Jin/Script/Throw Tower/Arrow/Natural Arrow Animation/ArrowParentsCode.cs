using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowParentsCode : MonoBehaviour
{
    public GameObject arrow;

    void Update()
    {
        // arrow 오브젝트가 비활성화되면 이 스크립트가 들어간 오브젝트도 비활성화합니다.
        if (arrow != null && !arrow.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
