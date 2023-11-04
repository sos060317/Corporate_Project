using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : MonoBehaviour
{
    private EnemyBase enemybase;
    private float attackPower;

    [HideInInspector] public FireArrowTower fireArrowTower;
    public ArrowTowerTemplate arrowTemplate;
    public UpgradeArrowTower ArrowUpgrade;
    private int aLevel = 0;


    public AnimationCurve curve;
    public float duration = 1.0f;
    public float maxHeightY = 3.0f;

    public string damageTag = "Enemy"; // �������� �� ����
    public float damageAreaRadius = 2.0f; // ���� ����
    public float Damage = 25;

    private Vector2 finish; // ���� ��ġ
    private bool reachedEnd = false;



    private void Start()
    {
        Vector3 start = transform.position;


        SetEnemyPositionAsFinish();
        StartCoroutine(Curve(start, finish));
    }

    private void Update()
    {
        aLevel = ArrowUpgrade.Arrowlevel;

        if (aLevel == 3)
        {
            Damage = arrowTemplate.aweapon[aLevel].aDamage;
        }


    }

    private void SetEnemyPositionAsFinish()
    {
        if (fireArrowTower.nowShot != false)
        {
            finish = fireArrowTower.EnemyPos;
        }

    }

    public IEnumerator Curve(Vector3 start, Vector2 finish)
    {
        float timePast = 0f;

        while (timePast < duration)
        {
            timePast += Time.deltaTime;

            float linearTime = timePast / duration;
            float heightTime = curve.Evaluate(linearTime);

            float height = Mathf.Lerp(0f, maxHeightY, heightTime);

            transform.position = Vector2.Lerp(start, finish, linearTime) + new Vector2(0f, height);

            yield return null;
        }

        if (!reachedEnd)
        {
            reachedEnd = true;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageAreaRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag(damageTag))
                {
                    collider.GetComponent<EnemyBase>().OnDamage(attackPower, Damage);  // ����? , ����? �𸣰ʹ�
                    collider.GetComponent<EnemyBase>().FireEnemy(3, 3, 5); // ����, �, ��ƽ
                    Destroy(gameObject);
                    break;
                }
            }

            yield return new WaitForSeconds(0.1f);

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, damageAreaRadius);
    }
}