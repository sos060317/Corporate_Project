using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceArrow : MonoBehaviour
{
    public Animator NextArrow;

    private EnemyBase enemybase;

    [HideInInspector] public IceArrowTower IceArrowTower;
    public ArrowTowerTemplate arrowTemplate;
    public UpgradeArrowTower ArrowUpgrade;
    public GameObject hitEffect;
    private int arrowLevel = 0;


    public AnimationCurve curve;
    public float duration = 1.0f;
    public float maxHeightY = 3.0f;

    public string damageTag = "Enemy"; // 데미지를 줄 옵젝
    public float damageAreaRadius = 2.0f; // 공격 범위
    private float Damage;

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
        if (GameManager.Instance.isGameStop)
        {
            NextArrow.StartPlayback();
            return;
        }
        else
        {
            NextArrow.StopPlayback();
        }

        arrowLevel = IceArrowTower.alevel;

        Damage = arrowTemplate.aweapon[arrowLevel].aDamage;

        //Debug.Log(arrowLevel);

        if (arrowLevel >= 1)
        {
            NextArrow.SetBool("NexrLevel1", true);
        }


    }

    private void SetEnemyPositionAsFinish()
    {
        //if (IceArrowTower.nowShot != false)
        //{
        //    finish = IceArrowTower.EnemyPos;
        //}

            finish = IceArrowTower.EnemyPos;

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
                    collider.GetComponent<EnemyBase>().OnDamage(0, Damage * GameManager.Instance.towerAttackDamageMultiply);  // 물리? , 마법? 모르것다
                    collider.GetComponent<EnemyBase>().SpeedDownEnemy(arrowTemplate.aweapon[arrowLevel].sllowTime, arrowTemplate.aweapon[arrowLevel].sllowextent); //1.5f ,0.7
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
