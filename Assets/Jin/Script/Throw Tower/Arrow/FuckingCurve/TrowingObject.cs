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
    public string enemyTag = "Enemy"; // ���� �±�

    private Vector2 finish; // ���� ��ġ
    private bool reachedEnd = false;

    private void Awake()
    {
        Vector3 start = transform.position;

        // ��ȯ�� �� ���� ��ǥ�� ���� ��ġ�� ����
        SetEnemyPositionAsFinish();

        StartCoroutine(Curve(start, finish));
    }

    private void SetEnemyPositionAsFinish()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        if (enemies.Length > 0)
        {
            finish = enemies[0].transform.position; // ù ��° ���� ��ǥ�� ���� ��ġ�� ����
        }
        else
        {
            // ���� ���� ��� ������ �⺻������ ����
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
