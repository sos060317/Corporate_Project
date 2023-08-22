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

    #region Header

    [Space(10)] [Header("아군 프리팹")]

    #endregion

    public GameObject allyPrefab;
}