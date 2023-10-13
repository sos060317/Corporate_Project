using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUpGrade : MonoBehaviour
{
    public Canvas BombUpGradeCanvas;
    private BombTower bombTower;

    private void Start()
    {
        bombTower = GetComponent<BombTower>();
        BombUpGradeCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(bombTower.isClick == true)
        {
            BombUpGradeCanvas.gameObject.SetActive(true);
        }
        else
        {
            BombUpGradeCanvas.gameObject.SetActive(false);
        }
    }
}
