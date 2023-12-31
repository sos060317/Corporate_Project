using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogThorn : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;

    [SerializeField] private float duration = 1.0f;
    [SerializeField] private float heightY = 3.0f;

    public void StartShot(Vector3 start, Vector2 target, AllyBase ally, float damage)
    {
        StartCoroutine(Curve(start, target, ally, damage));
    }

    private IEnumerator Curve(Vector3 start, Vector2 target, AllyBase ally, float damage)
    {
        float timePassed = 0f;

        Vector2 end = target;

        while (timePassed < duration)
        {
            if (Vector2.Distance(transform.position, ally.transform.position) <= 0.3f && timePassed > duration / 2)
            {
                StopAllCoroutines();
                ally.OnDamage(damage, 0);
                Destroy(gameObject);
            }
            
            timePassed += Time.deltaTime;

            float linearT = timePassed / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, heightY, heightT);

            Vector2 nextPos = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);
            Vector3 direction = transform.position - (Vector3)nextPos;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            
            transform.position = nextPos;
            
            yield return null;
        }
        
        Destroy(gameObject);
    }
}