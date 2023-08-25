using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrowingObject : MonoBehaviour
{
    //public AnimationCurve curve;
    //public float duration = 1.0f;
    //public float maxHeightY = 3.0f;
    //public string enemyTag = "Enemy"; // Tag of the enemy objects

    //private GameObject enemy; // Reference to the enemy object
    //private bool reachedEnd = false;

    //private void Awake()
    //{
    //    Vector3 start = transform.position;

    //    InvokeRepeating("SetEnemyPositionAsFinish", 0.3f, 0.3f);

    //    StartCoroutine(Curve(start, Vector2.zero));
    //}

    //private void SetEnemyPositionAsFinish()
    //{
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    //    if (enemies.Length > 0)
    //    {
    //        enemy = enemies[0]; // You might need more logic here to determine the appropriate enemy
    //        Vector2 finish = enemy.transform.position;
    //        StartCoroutine(Curve(transform.position, finish));
    //    }
    //}

    //public IEnumerator Curve(Vector3 start, Vector2 finish)
    //{
    //    float timePast = 0f;

    //    while (timePast < duration)
    //    {
    //        timePast += Time.deltaTime;

    //        float linearTime = timePast / duration;
    //        float heightTime = curve.Evaluate(linearTime);

    //        float height = Mathf.Lerp(0f, maxHeightY, heightTime);

    //        transform.position = Vector2.Lerp(start, finish, linearTime) + new Vector2(0f, height);

    //        yield return null;
    //    }

    //    // If the end is reached, destroy the object
    //    if (!reachedEnd)
    //    {
    //        reachedEnd = true;
    //        Destroy(gameObject);
    //    }
    //}

    public AnimationCurve curve;
    public float duration = 1.0f;
    public float maxHeightY = 3.0f;
    public string enemyTag = "Enemy"; // 적의 태그

    private Vector2 finish; // 종료 위치
    private bool reachedEnd = false;

    private void Awake()
    {
        Vector3 start = transform.position;

        // 소환될 때 적의 좌표를 종료 위치로 설정
        SetEnemyPositionAsFinish();

        StartCoroutine(Curve(start, finish));
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
            Destroy(gameObject);
        }
    }
}
