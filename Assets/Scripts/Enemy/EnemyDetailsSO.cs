using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/EnemyDetails")]
public class EnemyDetailsSO : ScriptableObject
{
    public string enemyName;
    
    #region Header

    [Space(10)] [Header("기본 스탯")]

    #endregion

    public float enemyBaseHealth;

    public float enemyBaseMoveSpeed;

    public float enemyBaseAttackRate;

    public float enemyBaseScanRange;

    public float enemyBaseAttackRange;

    #region Header

    [Space(10)] [Header("적 프리팹")]

    #endregion

    public GameObject enemyPrefab;
}