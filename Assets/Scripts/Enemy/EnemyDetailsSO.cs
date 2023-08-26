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

    public float enemyBaseAttackDamage;

    #region Header

    [Space(10)]
    [Header("방어력 관련")]

    #endregion

    [Range(0f, 100f)]
    public float defense;
    
    [Range(0f, 100f)]
    public float magicResistance;

    #region Header

    [Space(10)] [Header("공격력 관련")]

    #endregion

    public float attackPower;

    public float spellPower;

    #region Header

    [Space(10)] [Header("적 프리팹")]

    #endregion

    public GameObject enemyPrefab;
}