using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllyDetails_", menuName = "Scriptable Objects/Ally/AllyDetails")]
public class AllyDetailsSO : ScriptableObject
{
    public string allyName;

    #region Header

    [Space(10)] [Header("기본 스탯")]

    #endregion

    public float allyBaseHealth;

    public float allyBaseMoveSpeed;

    public float allyBaseAttackDelay;

    public float allyBaseAttackRange;

    public float allyBaseAttackDamage;
    
    #region Header

    [Space(10)] [Header("방어력 관련")]

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

    [Space(10)] [Header("아군 프리팹")]

    #endregion

    public GameObject allyPrefab;
}