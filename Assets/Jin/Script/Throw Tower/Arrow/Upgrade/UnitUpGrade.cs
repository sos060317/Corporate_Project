using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUpGrade : MonoBehaviour
{
    public Canvas UnitUpGradeCanvas;
    private GameUnitChake UnitTower;

    private void Start()
    {
        UnitTower = GetComponent<GameUnitChake>();
        UnitUpGradeCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(UnitTower.clicked == true)
        {
            UnitUpGradeCanvas.gameObject.SetActive(true);
        }
        else
        {
            UnitUpGradeCanvas.gameObject.SetActive(false);
        }
    }
}
