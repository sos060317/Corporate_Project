using UnityEngine;

[CreateAssetMenu(fileName = "StageEnemyCharacteristics", menuName = "Scriptable Objects/Stage/StageEnemyCharacteristics")]
public class StageEnemyCharacteristicsSO : ScriptableObject
{
    public string enemyName;

    #region Header

    [Space(10)]
    [Header("추가 기본 스탯")]

    #endregion

    public float enemyBaseHealth;

    public float enemyBaseMoveSpeed;

    public float enemyBaseAttackRate;

    public float enemyBaseAttackRange;

    #region Header

    [Space(10)]
    [Header("추가 방어력")]

    #endregion

    [Range(0f, 100f)]
    public float defense;

    [Range(0f, 100f)]
    public float magicResistance;

    #region Header

    [Space(10)]
    [Header("추가 공격력")]

    #endregion

    public float attackPower;

    public float spellPower;

    #region Header

    [Space(10)]
    [Header("UI 관련")]

    #endregion

    public Sprite profileImage;

    #region Header

    [Space(10)]
    [Header("코인 감소")]

    #endregion

    public int coins;

    #region Header

    [Space(10)]
    [Header("설명글")]

    #endregion

    public string changeStat;
}