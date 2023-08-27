using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyBase : MonoBehaviour
{
    [SerializeField] private LayerMask scanLayer;
    [SerializeField] private AllyDetailsSO allyDetailsSo;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Image healthUiBg;
    [SerializeField] private Image healthUiBar;
    
    [SerializeField] private float scanRange;

    [HideInInspector] public bool targeting;

    private float maxHealth;
    private float curHealth;
    private float moveSpeed;
    private float attackRange;
    private float attactRate;
    private float attactTimer;
    private float xScale;
    private float healthBgXScale;

    private bool isTargeting;
    private bool canMove;
    private bool isAttacking;
    private bool isDie;
    private bool isRun;
    private bool attack;

    private SpriteRenderer sr;
    private Animator anim;
    private WaitForSeconds hitDelay;
    
    private EnemyBase targetEnemy;

    private void OnEnable()
    {
        attactTimer = 0f;
        isDie = false;
    }

    private void Start()
    {
        // 컴포넌트 할당
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        // 변수 초기화
        isTargeting = false;
        canMove = true;
        isAttacking = false;
        isDie = false;
        isRun = false;
        hitDelay = new WaitForSeconds(0.1f);

        moveSpeed = allyDetailsSo.allyBaseMoveSpeed;
        attackRange = allyDetailsSo.allyBaseAttackRange;
        attactRate = allyDetailsSo.allyBaseAttackDelay;
        xScale = transform.localScale.x;
        healthBgXScale = healthUiBar.rectTransform.localScale.x;

        maxHealth = allyDetailsSo.allyBaseHealth;
        curHealth = maxHealth;
    }

    private void Update()
    {
        MoveUpdate();
        AttackUpdate();
        AnimationUpdate();
    }

    private void FixedUpdate()
    {
        CheckTarget();
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
        
        if (isTargeting)
        {
            var dir = targetEnemy.transform.position - transform.position;
            transform.position += dir.normalized * (moveSpeed * Time.deltaTime);
            
            if (dir.x < 0)
            {
                FilpFunction(-1);
            }
            else
            {
                FilpFunction(1);
            }

            isRun = true;

            if (Vector2.Distance(transform.position, targetEnemy.transform.position) <= attackRange)
            {
                canMove = false;
                isAttacking = true;
                isRun = false;
            }
        }
    }


    private void FilpFunction(int index)
    {
        transform.localScale =
            new Vector3(xScale * index, transform.localScale.y, transform.localScale.z);

        healthUiBg.rectTransform.localScale = new Vector3(healthBgXScale * index, healthUiBg.rectTransform.localScale.y,
            healthUiBg.rectTransform.localScale.z);
    }
    
    private void AttackUpdate()
    {
        if (!isAttacking || targetEnemy == null)
        {
            return;
        }

        if (attack)
        {
            return;
        }

        if (attactTimer >= attactRate && !isDie)
        {
            // 공격 로직
            
            anim.SetTrigger("Attack");
            
            attactTimer = 0f;

            attack = true;
        }

        attactTimer += Time.deltaTime;
    }

    private void AnimationUpdate()
    {
        anim.SetBool("IsRun", isRun);
    }

    private void CheckTarget()
    {
        if (isTargeting)
        {
            return;
        }
        
        var target = Physics2D.OverlapCircle(transform.position, scanRange, scanLayer);

        if (target != null)
        {
            if (!target.GetComponent<EnemyBase>().Targeting)
            {
                targetEnemy = target.gameObject.GetComponent<EnemyBase>();
                targetEnemy.SetTarget(this);
                targetEnemy.Targeting = true;
                isTargeting = true;
            }
        }
    }
    
    // 애니메이션 이벤트에서 사용할 함수
    private void AttackEnd()
    {
        attack = false;
    }
    
    // 애니메이션 이벤트에서 사용할 함수.
    private void AttackDamage()
    {
        if (targetEnemy == null)
        {
            return;
        }
        
        targetEnemy.OnDamage(allyDetailsSo.attackPower, allyDetailsSo.spellPower);
    }
    
    private IEnumerator HitRoutine()
    {
        sr.material = hitMaterial;

        yield return hitDelay;

        sr.material = defaultMaterial;
    }
    
    public void DeleteTarget()
    {
        isTargeting = false;
        canMove = true;
        isAttacking = false;
        attack = false;
        targetEnemy = null;
    }

    public void OnDamage(float attackPower, float spellPower)
    {
        curHealth -= (attackPower - (attackPower * (allyDetailsSo.defense * 0.01f))) + (spellPower - (spellPower * (allyDetailsSo.magicResistance * 0.01f)));
        
        if (curHealth <= 0)
        {
            // 죽는 로직

            isDie = true;
            targetEnemy.DeleteTarget();
            anim.SetTrigger("Die");
            return;
        }
        
        StartCoroutine(HitRoutine());
    }
    
    // 애니메이션 이벤트에 사용할 함수.
    private void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

    public void SetMoveAnimation(bool isMoving, Vector3 dir)
    {
        if (isMoving)
        {
            canMove = false;
            isAttacking = false;
        }
        else
        {
            canMove = true;
        }

        isRun = isMoving;
        
        if (dir.x < 0)
        {
            FilpFunction(-1);
        }
        else
        {
            FilpFunction(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}