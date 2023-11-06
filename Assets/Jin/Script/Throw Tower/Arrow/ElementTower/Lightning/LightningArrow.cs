using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningArrow : MonoBehaviour
{
    private EnemyBase enemybase;
    private float attackPower;

    [HideInInspector] public LightningArrowTower lightningArrowTower;
    public ArrowTowerTemplate arrowTemplate;
    public UpgradeArrowTower ArrowUpgrade;
    private int aLevel = 0;


    public AnimationCurve curve;
    public float duration = 1.0f;
    public float maxHeightY = 3.0f;

    public string damageTag = "Enemy"; // 데미지를 줄 옵젝
    public float damageAreaRadius = 2.0f; // 공격 범위
    public float Damage = 25;

    private Vector2 finish; // 종료 위치
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
        if (lightningArrowTower.nowShot != false)
        {
            finish = lightningArrowTower.EnemyPos;
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
                    collider.GetComponent<EnemyBase>().OnDamage(attackPower, Damage);  // 물리? , 마법? 모르것다
                    collider.GetComponent<EnemyBase>().FaintEnemy(0.3f);
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
