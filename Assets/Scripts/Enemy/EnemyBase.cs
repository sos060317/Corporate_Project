using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Image healthUiBg;
    [SerializeField] private Image healthUiBar;

    [HideInInspector] public bool Targeting;
    
    private int movePosIndex;

    private float maxHealth;
    private float curHealth;
    private float attackRate;
    private float attackTimer;
    private float attackRange;
    private float moveSpeed;
    private float attackDamage;
    private float xScale;
    private float healthBgXScale;
    
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
    private WaitForSeconds hitDelay;

    private AllyBase targetAlly;

    private void Start()
    {
        // 컴포넌트 할당
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        // 변수 초기화
        hitDelay = new WaitForSeconds(0.1f);
        xScale = transform.localScale.x;
        healthBgXScale = healthUiBar.rectTransform.localScale.x;
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
    
    private void LateUpdate()
    {
        HealthUpdate();
    }
    
    private void HealthUpdate()
    {
        healthUiBar.fillAmount = Mathf.Lerp(healthUiBar.fillAmount, curHealth / maxHealth, Time.deltaTime * 12);
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
                transform.localScale =
                    new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
                
                healthUiBg.rectTransform.localScale = new Vector3(-healthBgXScale, healthUiBg.rectTransform.localScale.y,
                    healthUiBg.rectTransform.localScale.z);
            }
            else
            {
                transform.localScale =
                    new Vector3(xScale, transform.localScale.y, transform.localScale.z);
                
                healthUiBg.rectTransform.localScale = new Vector3(healthBgXScale, healthUiBg.rectTransform.localScale.y,
                    healthUiBg.rectTransform.localScale.z);
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
            transform.localScale =
                new Vector3(-xScale, transform.localScale.y, transform.localScale.z);
            
            healthUiBg.rectTransform.localScale = new Vector3(-healthBgXScale, healthUiBg.rectTransform.localScale.y,
                healthUiBg.rectTransform.localScale.z);
        }
        else
        {
            transform.localScale =
                new Vector3(xScale, transform.localScale.y, transform.localScale.z);
            
            healthUiBg.rectTransform.localScale = new Vector3(healthBgXScale, healthUiBg.rectTransform.localScale.y,
                healthUiBg.rectTransform.localScale.z);
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

        if (attackTimer >= attackRate && !isDie)
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
    
    private IEnumerator HitRoutine()
    {
        sr.material = hitMaterial;

        yield return hitDelay;

        sr.material = defaultMaterial;
    }
    
    // 애니메이션 이벤트에 사용할 함수.
    private void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

    public void SetTarget(AllyBase ally)
    {
        targetAlly = ally;
        isTargeting = true;
        Targeting = true;
    }

    public void DeleteTarget()
    {
        isTargeting = false;
        canMove = true;
        isAttacking = false;
        attack = false;
        targetAlly = null;
        Targeting = false;
    }

    public void OnDamage(float damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            // 죽는 로직

            isDie = true;
            targetAlly.DeleteTarget();
            anim.SetTrigger("Die");
            return;
        }
        
        StartCoroutine(HitRoutine());
    }

    public void InitEnemy(Vector2[] movePoints, EnemyDetailsSO enemyDetailsSo)
    {
        this.movePoints = movePoints;
        this.enemyDetailsSo = enemyDetailsSo;

        maxHealth = this.enemyDetailsSo.enemyBaseHealth;
        moveSpeed = this.enemyDetailsSo.enemyBaseMoveSpeed;
        attackRate = this.enemyDetailsSo.enemyBaseAttackRate;
        attackRange = this.enemyDetailsSo.enemyBaseAttackRange;
        attackDamage = this.enemyDetailsSo.enemyBaseAttackDamage;


        curHealth = maxHealth;
        movePosIndex = 0;
        
        moveOffset = new Vector3(0, Random.Range(-1f, 1f));

        transform.position = movePoints[movePosIndex] + moveOffset;

        canMove = true;
    }
}