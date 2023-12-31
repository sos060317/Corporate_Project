using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrow : MonoBehaviour
{
    public Animator NextArrow;

    private EnemyBase enemybase;

    [HideInInspector] public PoisonArrowTower poisonArrowTower;
    public ArrowTowerTemplate arrowTemplate;
    public UpgradeArrowTower ArrowUpgrade;
    private int arrowLevel = 0;


    public AnimationCurve curve;
    public float duration = 1.0f;
    public float maxHeightY = 3.0f;

    public string damageTag = "Enemy"; // 데미지를 줄 옵젝
    public float damageAreaRadius = 2.0f; // 공격 범위
    public float Damage = 25;
    public GameObject hitEffect;

    private Vector2 finish; // 종료 위치
    private bool reachedEnd = false;

    private float PoisonDamage;



    private void Start()
    {
        Vector3 start = transform.position;


        SetEnemyPositionAsFinish();
        StartCoroutine(Curve(start, finish));

        PoisonDamage = arrowTemplate.aweapon[arrowLevel].poisonDamage;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStop)
        {
            NextArrow.StartPlayback();
            return;
        }
        else
        {
            NextArrow.StopPlayback();
        }

        arrowLevel = poisonArrowTower.alevel;

        Damage = arrowTemplate.aweapon[arrowLevel].aDamage;

        if (arrowLevel >= 1)
        {
            NextArrow.SetBool("NexrLevel1", true);
        }
    }

    private void SetEnemyPositionAsFinish()
    {

        finish = poisonArrowTower.EnemyPos;

        if (finish == Vector2.zero)
        {
            Destroy(gameObject);
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
                    collider.GetComponent<EnemyBase>().OnDamage(0, Damage * GameManager.Instance.towerAttackDamageMultiply);
                    collider.GetComponent<EnemyBase>().PoisonEnemy(arrowTemplate.aweapon[arrowLevel].poisonTime, PoisonDamage * GameManager.Instance.towerAttackDamageMultiply, arrowTemplate.aweapon[arrowLevel].poisonCount); // 초, 뎀지, 틱사이 시간
                    Instantiate(hitEffect, collider.transform);
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
