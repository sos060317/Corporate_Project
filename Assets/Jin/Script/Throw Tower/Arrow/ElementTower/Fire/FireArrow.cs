using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : MonoBehaviour
{
    public Animator NextArrow;

    private EnemyBase enemybase;

    [HideInInspector] public FireArrowTower fireArrowTower;
    public ArrowTowerTemplate arrowTemplate;
    public UpgradeArrowTower ArrowUpgrade;
    private int arowLevel = 0;


    public AnimationCurve curve;
    public float duration = 1.0f;
    public float maxHeightY = 3.0f;

    public string damageTag = "Enemy"; // �������� �� ����
    public float damageAreaRadius = 2.0f; // ���� ����
    private float Damage;

    private Vector2 finish; // ���� ��ġ
    private bool reachedEnd = false;

    public float TickDamage;
    public float TickTime;

    private float FireDamage;

    private void Start()
    {
        Vector3 start = transform.position;


        SetEnemyPositionAsFinish();
        StartCoroutine(Curve(start, finish));

        FireDamage = arrowTemplate.aweapon[arowLevel].fireDamage;
    }

    private void Update()
    {

        
        arowLevel = fireArrowTower.alevel;

        Damage = arrowTemplate.aweapon[arowLevel].aDamage;

        Debug.Log(arowLevel);

        if (arowLevel >= 1)
        {
            NextArrow.SetBool("NextLevel1", true);
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
                    collider.GetComponent<EnemyBase>().OnDamage(Damage * GameManager.Instance.towerAttackDamageMultiply, 0);  // ����? , ����? �𸣰ʹ�
                    collider.GetComponent<EnemyBase>().FireEnemy(arrowTemplate.aweapon[arowLevel].fireTime, FireDamage * GameManager.Instance.towerAttackDamageMultiply, arrowTemplate.aweapon[arowLevel].fireCount); // ����, �, ��ƽ
                    
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
