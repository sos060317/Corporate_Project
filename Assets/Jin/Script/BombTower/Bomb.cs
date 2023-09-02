using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private EnemyBase enemybase;
    private float attackPower = 20;

    public AnimationCurve curve;
    public float duration = 1.0f;
    public float maxHeightY = 3.0f;
    public string enemyTag = "FlowingPos"; // ���� �±�

    public string DamageTag = "Enemy";
    public float damageAreaRadius = 2.0f; // ���� ���� �ݰ�
    public float Damage = 50f;

    private Vector2 finish; // ���� ��ġ
    private bool reachedEnd = false;

    private bool damageDealt = false; // �������� �̹� �־����� ���� Ȯ�� ����

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
        Vector3 originalScale = transform.localScale; // ���� ũ�� ����

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
            float scaleFactor = 0.1f; // ������ �۾��� ����
            float scaleTime = 0f;

            while (scaleTime < 0.2f) // 0.2�� ���� �۾����� ������ ����
            {
                scaleTime += Time.deltaTime;
                float shrinkFactor = Mathf.Lerp(1f, scaleFactor, scaleTime / 0.2f);
                transform.localScale = originalScale * shrinkFactor;
                yield return null;
            }

            scaleTime = 0f;
            while (scaleTime < 0.3f) // 0.3�� ���� Ŀ���� ������ ����
            {
                scaleTime += Time.deltaTime;
                float growthFactor = Mathf.Lerp(scaleFactor, 3f, scaleTime / 0.3f);
                transform.localScale = originalScale * growthFactor;

                // �������� ���� ���� �ʾҴٸ�
                if (!damageDealt)
                {
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageAreaRadius);

                    foreach (Collider2D collider in colliders)
                    {
                        if (collider.CompareTag(DamageTag))
                        {
                            Debug.Log("How");
                            collider.GetComponent<EnemyBase>().OnDamage(attackPower, Damage);
                            // enemybase.OnDamage(attackPower, Damage);
                            // OnDamage �Լ� ȣ�� �ڵ尡 �ʿ��մϴ�.
                            damageDealt = true; // ������ �־����� ǥ��
                            break; // �� �̻� Ȯ���� �ʿ䰡 �����Ƿ� ���� ����
                        }
                    }
                }

                yield return null;
            }

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, damageAreaRadius);
    }

}