using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrowingObject : MonoBehaviour
{
    private EnemyBase enemybase;
    private float attackPower;

    public ArrowTowerTemplate arrowTemplate;
    private UpgradeArrowTower ArrowUpgrade;
    private int aLevel = 0; 


    public AnimationCurve curve;
    public float duration = 1.0f;
    public float maxHeightY = 3.0f;
    public string enemyTag = "FlowingPos"; // 적의 태그

    public string damageTag = "Enemy"; // 데미지를 줄 옵젝
    public float damageAreaRadius = 2.0f; // 공격 범위
    public float Damage = 25;

    private Vector2 finish; // 종료 위치
    private bool reachedEnd = false;

    private void Awake()
    {
        Vector3 start = transform.position;

        // 소환될 때 적의 좌표를 종료 위치로 설정
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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if (enemies.Length > 0)
        {
            finish = enemies[0].transform.position; // 첫 번째 적의 좌표를 종료 위치로 설정
        }
        else
        {
            // 적이 없을 경우 임의의 기본값으로 설정
            finish = Vector2.zero;
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
                    collider.GetComponent<EnemyBase>().OnDamage(attackPower, Damage);
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