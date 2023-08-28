using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAndDamage : MonoBehaviour
{
    //public string enemyTag = "FlowingPos";
    //public float speed = 10f;
    //public float initialDelay = 0.2f;

    //private Transform target;
    //private Vector3 targetPosition;
    //private bool isFollowing = true;

    //private void Start()
    //{
    //    Invoke("StopFollowing", initialDelay);
    //}

    //private void StopFollowing()
    //{
    //    isFollowing = false;

    //    GameObject[] flowingObjects = GameObject.FindGameObjectsWithTag(enemyTag);
    //    if (flowingObjects.Length > 0)
    //    {
    //        target = flowingObjects[0].transform;
    //        targetPosition = target.position;
    //        isFollowing = true;
    //    }
    //    else
    //    {
    //        // FlowingPos1 태그를 가진 오브젝트가 없을 경우 처리할 내용 추가
    //        Destroy(gameObject); // 예시로 스크립트가 적용된 오브젝트를 파괴
    //    }
    //}

    //private void Update()
    //{
    //    if (isFollowing && target != null)
    //    {
    //        Vector3 moveDirection = (targetPosition - transform.position).normalized;
    //        transform.position += moveDirection * speed * Time.deltaTime;

    //        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}


    private EnemyBase enemybase;
    private float spellPower = 20;

    public string enemyTag = "FlowingPos";
    public float speed = 10f;
    public float initialDelay = 0.2f;

    public string DamageTag = "Enemy";
    public float damageAreaRadius = 2.0f; // 공격 범위 반경
    public float Damage = 30;

    private Transform target;
    private Vector3 targetPosition;
    private bool isFollowing = true;

    private void Start()
    {
        Invoke("StopFollowing", initialDelay);
    }

    private void StopFollowing()
    {
        isFollowing = false;

        GameObject[] flowingObjects = GameObject.FindGameObjectsWithTag(enemyTag);
        if (flowingObjects.Length > 0)
        {
            target = flowingObjects[0].transform;
            targetPosition = target.position;
            isFollowing = true;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Update()
    {
        if (isFollowing && target != null)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {

                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageAreaRadius);

                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag(DamageTag))
                    {
                        Debug.Log("How");
                        collider.GetComponent<EnemyBase>().OnDamage(0, spellPower);
                        //enemybase.OnDamage(spellPower, Damage);
                    }
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, damageAreaRadius);
    }
}
