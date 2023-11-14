using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public BombTowerTemplate BombTemplate;
    public Animator Bombanim;

    public BombTower bombTower;

    private EnemyBase enemybase;
    private float attackPower;

    public AnimationCurve curve;
    public float duration = 1.0f;
    public float maxHeightY = 3.0f;
    public GameObject hitEffect;
    //public string enemyTag = "FlowingPos"; // 적의 태그
    private int Blaval;

    public string DamageTag = "Enemy";
    public float damageAreaRadius = 2.0f; // 공격 범위 반경
    public float Damage;

    private Vector2 finish; // 종료 위치

    private bool damageDealt = false; // 데미지를 이미 주었는지 여부 확인 변수

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
            Bombanim.StartPlayback();
            return;
        }
        else
        {
            Bombanim.StopPlayback();
        }
    }

    private void SetEnemyPositionAsFinish()
    {
        Blaval = bombTower.BombLevel;

        damageAreaRadius = BombTemplate.Bweapon[Blaval].damageRound;

        if (bombTower.BombShot != false)
        {
            finish = bombTower.BEnemyPos;
        }

        if (finish == Vector2.zero)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Curve(Vector3 start, Vector2 finish)
    {
        float timePast = 0f;
        Vector3 originalScale = transform.localScale; // 원래 크기 저장

        while (timePast < duration)
        {
            timePast += Time.deltaTime;

            float linearTime = timePast / duration;
            float heightTime = curve.Evaluate(linearTime);

            float height = Mathf.Lerp(0f, maxHeightY, heightTime);

            transform.position = Vector2.Lerp(start, finish, linearTime) + new Vector2(0f, height);

            yield return null;
        }

        if (!damageDealt)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageAreaRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag(DamageTag))
                {
                    collider.GetComponent<EnemyBase>().OnDamage(attackPower, Damage);
                    damageDealt = true; // 데미지 주었음을 표시
                }
            }
        }

        Destroy(gameObject);

        //if (!reachedEnd)
        //{
        //    reachedEnd = true;
        //    float scaleFactor = 0.1f; // 시작할 작아짐 비율
        //    float scaleTime = 0f;

        //    while (scaleTime < 0.2f) // 0.2초 동안 작아지는 동안의 루프
        //    {
        //        scaleTime += Time.deltaTime;
        //        float shrinkFactor = Mathf.Lerp(1f, scaleFactor, scaleTime / 0.2f);
        //        transform.localScale = originalScale * shrinkFactor;
        //        yield return null;
        //    }

        //    scaleTime = 0f;
        //    while (scaleTime < 0.3f) // 0.3초 동안 커지는 동안의 루프
        //    {
        //        scaleTime += Time.deltaTime;
        //        float growthFactor = Mathf.Lerp(scaleFactor, 3f, scaleTime / 0.3f);
        //        transform.localScale = originalScale * growthFactor;

        //        // 데미지를 아직 주지 않았다면
        //        if (!damageDealt)
        //        {
        //            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageAreaRadius);

        //            foreach (Collider2D collider in colliders)
        //            {
        //                if (collider.CompareTag(DamageTag))
        //                {
        //                    collider.GetComponent<EnemyBase>().OnDamage(attackPower, Damage);
        //                    damageDealt = true; // 데미지 주었음을 표시
        //                }
        //            }
        //        }

        //        yield return null;
        //    }

        //    Destroy(gameObject);
        //}
    }

    private void FixedUpdate() 
    {
        Damage = BombTemplate.Bweapon[bombTower.BombLevel].damage; 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, damageAreaRadius);
    }

}