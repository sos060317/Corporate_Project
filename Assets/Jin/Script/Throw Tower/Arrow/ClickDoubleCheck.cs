using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDoubleCheck : MonoBehaviour
{
    public bool ClickdoubleChck = false;

    private void Update()
    {
        if (ClickdoubleChck == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                ClickdoubleChck = false;
            }
        }
    }

    private void OnMouseDown()
    {
        ClickdoubleChck = true;
    }
}
