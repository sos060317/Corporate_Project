using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private LayerMask scanLayer;

    [HideInInspector] public bool Targeting;
    
    private int movePosIndex;

    private float maxHealth;
    private float curHelath;
    private float attackRate;
    private float attackTimer;
    private float scanRange;
    private float attackRange;
    private float moveSpeed;
    private float attackDamage;
    
    private bool canMove = true;
    private bool isTargeting = false;
    private bool isAttacking = false;
    private bool isDie = false;
    private bool attack = false;
    
    private Vector2[] movePoints;
    private Vector2 moveOffset;

    private SpriteRenderer sr;
    private EnemyDetailsSO enemyDetailsSo;
    private Animator anim;

    private AllyBase targetAlly;

    private void Start()
    {
        // Load Component
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        attackTimer = 0f;
        isDie = false;
        Targeting = false;
    }

    private void Update()
    {
        MoveUpdate();
        AttackUpdate();
        AnimationUpdate();
    }

    private void MoveUpdate()
    {
        if (!canMove)
        {
            return;
        }
        
        // 주변에 타겟이 있을때
        if (isTargeting)
        {
            Vector3 dir = targetAlly.transform.position - transform.position;
            transform.position += dir.normalized * (moveSpeed * Time.deltaTime);
            
            if (dir.x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

            if (Vector2.Distance(transform.position, targetAlly.transform.position ) <= attackRange)
            {
                canMove = false;
                isAttacking = true;
            }
            
            return;
        }
        
        Vector3 nextPos = (Vector3)movePoints[movePosIndex] - transform.position + (Vector3)moveOffset;
        transform.position += nextPos.normalized * (moveSpeed * Time.deltaTime);
        
        if (nextPos.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        if (Vector2.Distance(transform.position - (Vector3)(moveOffset), movePoints[movePosIndex]) <= 0.01f)
        {
            movePosIndex++;
        }

        if (movePosIndex >= movePoints.Length)
        {
            //GameManager.Instance.defianceLife--;
            Destroy(gameObject);
        }
    }
    
    private void AnimationUpdate()
    {
        anim.SetBool("isRun", canMove);
    }
    
    private void AttackUpdate()
    {
        if (!isAttacking || targetAlly == null)
        {
            return;
        }
        
        // 아군을 죽였으면 다시 움직이게 하는 로직
        if (targetAlly.CheckAllyIsDie())
        {
            isTargeting = false;
            canMove = true;
            isAttacking = false;
            attack = false;
            targetAlly = null;
            Targeting = true;
        }

        if (attackTimer >= attackRate)
        {
            // 공격 로직
            
            anim.SetTrigger("Attack");
            
            attackTimer = 0f;

            attack = true;
        }

        if (attack)
        {
            return;
        }
        
        attackTimer += Time.deltaTime;
    }
    
    // 애니메이션 이벤트에서 사용할 함수.
    private void AttackEnd()
    {
        attack = false;
    }
    
    // 애니메이션 이벤트에서 사용할 함수.
    private void AttackDamage()
    {
        if (targetAlly == null)
        {
            return;
        }
        
        targetAlly.OnDamage(attackDamage);
    }

    public void SetTarget(AllyBase ally)
    {
        targetAlly = ally;
        isTargeting = true;
        Targeting = true;
    }

    public void OnDamage(float damage)
    {
        curHelath -= damage;

        if (curHelath <= 0)
        {
            // 죽는 로직

            isDie = true;
            targetAlly.targeting = false;
            gameObject.SetActive(false);
            Targeting = false;
        }
    }
    
    public bool CheckEnemyIsDie()
    {
        return isDie;
    }

    public void InitEnemy(Vector2[] movePoints, EnemyDetailsSO enemyDetailsSo)
    {
        this.movePoints = movePoints;
        this.enemyDetailsSo = enemyDetailsSo;

        maxHealth = this.enemyDetailsSo.enemyBaseHealth;
        moveSpeed = this.enemyDetailsSo.enemyBaseMoveSpeed;
        scanRange = this.enemyDetailsSo.enemyBaseScanRange;
        attackRate = this.enemyDetailsSo.enemyBaseAttackRate;
        attackRange = this.enemyDetailsSo.enemyBaseAttackRange;
        attackDamage = this.enemyDetailsSo.enemyBaseAttackDamage;


        curHelath = maxHealth;
        movePosIndex = 0;
        
        moveOffset = new Vector3(0, Random.Range(-1f, 1f));

        transform.position = movePoints[movePosIndex] + moveOffset;

        canMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}