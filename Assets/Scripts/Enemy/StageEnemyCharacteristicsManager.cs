using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemyCharacteristicsManager : MonoBehaviour
{
    [SerializeField] private StageEnemyCharacteristicsSO ch;

    private static StageEnemyCharacteristicsManager instance = null;

    public static StageEnemyCharacteristicsManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
