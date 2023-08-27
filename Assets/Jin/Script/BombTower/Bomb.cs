using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 1.0f;
    public float maxHeightY = 3.0f;
    public string enemyTag = "FlowingPos"; // 적의 태그

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
            finish = enemies[0].transform.position;
        }
        else
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

        if (!reachedEnd)
        {
            reachedEnd = true;
            float scaleFactor = 0.1f; // 시작할 작아짐 비율
            float scaleTime = 0f;

            while (scaleTime < 0.2f) // 0.2초 동안 작아지는 동안의 루프
            {
                scaleTime += Time.deltaTime;
                float shrinkFactor = Mathf.Lerp(1f, scaleFactor, scaleTime / 0.2f);
                transform.localScale = originalScale * shrinkFactor;
                yield return null;
            }

            scaleTime = 0f;
            while (scaleTime < 0.3f) // 0.8초 동안 커지는 동안의 루프
            {
                scaleTime += Time.deltaTime;
                float growthFactor = Mathf.Lerp(scaleFactor, 3f, scaleTime / 0.3f);
                transform.localScale = originalScale * growthFactor;
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}
