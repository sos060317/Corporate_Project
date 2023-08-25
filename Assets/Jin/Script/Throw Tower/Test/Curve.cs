using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{
    //public AnimationCurve curve;

    //[SerializeField]
    //private float duration = 1.0f;

    //[SerializeField]
    //private float heightY = 3.0f;

    //public IEnumerator Curves(Vector3 start, Vector2 target)
    //{
    //    float timePassed = 0f;

    //    Vector2 end = target;

    //    while (timePassed < duration)
    //    {
    //        timePassed += Time.deltaTime;

    //        float linearT = timePassed / duration;
    //        float heightT = curve.Evaluate(linearT);

    //        float height = Mathf.Lerp(0f, heightY, heightT);

    //        transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);

    //        yield return null;
    //    }
    //}

    public AnimationCurve curve;

    [SerializeField]
    private float duration = 1.0f;

    [SerializeField]
    private float heightY = 3.0f;

    [SerializeField]
    private float moveSpeed = 1.0f;

    private Transform target;
    private Vector3 spawnPosition;
    private System.Action onHitCallback;

    public void SetTarget(Transform newTarget, Vector3 spawnPos, System.Action callback)
    {
        target = newTarget;
        spawnPosition = spawnPos;
        onHitCallback = callback;

        StartCoroutine(MoveCurve());
    }

    private IEnumerator MoveCurve()
    {
        float timePassed = 0f;
        Vector2 start = spawnPosition;
        Vector2 end = target.position;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime * moveSpeed;

            float linearT = timePassed / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);

            yield return null;
        }

        if (onHitCallback != null)
        {
            onHitCallback.Invoke();
        }

        Destroy(gameObject);
    }
}
