using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAndDamage : MonoBehaviour
{
    [HideInInspector] public TowerSetting MagicTw;
    public TowerSetting MagicTower;
    public MagicTowerTemplate magicTemplate;
    public MagicPosCheck magicLevel;

    private int mLevel;
    public float speed = 10f;

    private string DamageTag = "Enemy";
    public float damageAreaRadius = 2.0f; // 공격 범위 반경
    private float Damage;

    private Vector2 targetPosition; // 목표 위치를 Vector2로 변경
    private bool isFollowing = true;

    private float delayTimer = .5f;

    public Animator MagicAnim;

    private void Start()
    {
        mLevel = magicLevel.Magiclevel;
        Damage = magicTemplate.mweapon[mLevel].damage;

        //if (MagicTower != null && MagicTower.MEnemyPos != Vector2.zero)
        //{
        //    targetPosition = MagicTower.MEnemyPos;
        //}

        StartCoroutine(DelayedStart());
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStop)
        {
            MagicAnim.StartPlayback();
            return;
        }
        else
        {
            MagicAnim.StopPlayback();
        }

        if (isFollowing)
        {
            delayTimer -= Time.deltaTime;

            if (delayTimer <= 0f)
            {
                MagicAnim.SetBool("Shot", true);

                Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
                Vector2 moveDirection = (targetPosition - currentPosition).normalized;
                transform.position += new Vector3(moveDirection.x, moveDirection.y, 0) * speed * Time.deltaTime;

                float distanceToTarget = Vector2.Distance(currentPosition, targetPosition);

                if (distanceToTarget < 0.1f)
                {
                    // 도착 지점에 도달하면 멈춤
                    isFollowing = false;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageAreaRadius);

                    foreach (Collider2D collider in colliders)
                    {
                        if (collider.CompareTag(DamageTag))
                        {
                            collider.GetComponent<EnemyBase>().OnDamage(0, Damage);
                            break;
                        }
                    }

                    Destroy(gameObject);
                }
            }

        }
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(.5f); // Wait for 1 second

        mLevel = magicLevel.Magiclevel;
        Damage = magicTemplate.mweapon[mLevel].damage;

        if (MagicTower != null && MagicTower.MEnemyPos != Vector2.zero)
        {
            targetPosition = MagicTower.MEnemyPos;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, damageAreaRadius);
    }
}
