using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUpGrade : MonoBehaviour
{
    public Canvas UpGradeCanvas;
    private Spawn_j arrowTower;

    private void Start()
    {
        arrowTower = GetComponent<Spawn_j>();
        UpGradeCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(arrowTower.isClicked == true)
        {
            UpGradeCanvas.gameObject.SetActive(true);
        }
        else
        {
            UpGradeCanvas.gameObject.SetActive(false);
        }
    }
}
