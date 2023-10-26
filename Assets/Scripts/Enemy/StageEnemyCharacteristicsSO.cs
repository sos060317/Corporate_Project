using UnityEngine;

[CreateAssetMenu(fileName = "StageEnemyCharacteristics", menuName = "Scriptable Objects/Stage/StageEnemyCharacteristics")]
public class StageEnemyCharacteristicsSO : ScriptableObject
{
    public string enemyName;

    #region Header

    [Space(10)]
    [Header("�߰� �⺻ ����")]

    #endregion

    public float enemyBaseHealth;

    public float enemyBaseMoveSpeed;

    public float enemyBaseAttackRate;

    public float enemyBaseAttackRange;

    #region Header

    [Space(10)]
    [Header("�߰� ����")]

    #endregion

    [Range(0f, 100f)]
    public float defense;

    [Range(0f, 100f)]
    public float magicResistance;

    #region Header

    [Space(10)]
    [Header("�߰� ���ݷ�")]

    #endregion

    public float attackPower;

    public float spellPower;

    #region Header

    [Space(10)]
    [Header("UI ����")]

    #endregion

    public Sprite profileImage;

    #region Header

    [Space(10)]
    [Header("���� ����")]

    #endregion

    public int coins;

    #region Header

    [Space(10)]
    [Header("�����")]

    #endregion

    public string changeStat;
}