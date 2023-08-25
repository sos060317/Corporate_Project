using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gokson : MonoBehaviour
{
    public AnimationCurve curve;

    public Vector3 spawnPosition;
    public Transform target;

    public void Initialize(Vector3 spawnPos, Transform targetTransform)
    {
        spawnPosition = spawnPos;
        target = targetTransform;

        StartCoroutine(MoveCurve());
    }

    private IEnumerator MoveCurve()
    {
        float timePassed = 0f;
        Vector3 start = spawnPosition;
        Vector3 end = target.position;

        while (timePassed < curve[curve.length - 1].time)
        {
            timePassed += Time.deltaTime;

            float linearT = curve.Evaluate(timePassed);
            float height = curve.Evaluate(linearT);

            transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0f, height);

            yield return null;
        }

        Destroy(gameObject);
    }
}
