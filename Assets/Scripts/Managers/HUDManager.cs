using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private static HUDManager instance = null;

    public static HUDManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }

            return instance;
        }
    }

    [SerializeField] private EnemyStatusWindow enemyStatusWindow;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowEnemyWindow(EnemyBase enemyBase)
    {
        enemyStatusWindow.ShowWindow(enemyBase);
    }

    public void ShowAllyWindow(AllyBase allyBase)
    {
        enemyStatusWindow.ShowWindow(allyBase);
    }
}