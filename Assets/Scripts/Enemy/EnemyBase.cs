using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Image healthUiBg;
    [SerializeField] private Image healthUiBar;

     public bool Targeting;
    
    private int movePosIndex;

    private float maxHealth;
    private float moveSpeed;
    private float xScale;
    private float healthBgXScale;
    
    protected bool canMove = true;
    protected bool isTargeting = false;
    protected bool isAttacking = false;
    protected bool isDie = false;
    protected bool attack = false;
    
    private Vector2[] movePoints;
    private Vector2 moveOffset;

    private SpriteRenderer sr;
    private WaitForSeconds hitDelay;
    
    protected float curHealth;
    protected float attackRate;
    protected float attackTimer;
    protected float attackRange;
    
    protected EnemyDetailsSO enemyDetailsSo;
    
    protected Animator anim;

    protected AllyBase targetAlly;

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

    protected virtual void Update()
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
                FlipFunction(-1);
            }
            else
            {
                FlipFunction(1);
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
            FlipFunction(-1);
        }
        else
        {
            FlipFunction(1);
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
    
    private void FlipFunction(int index)
    {
        transform.localScale =
            new Vector3(xScale * index, transform.localScale.y, transform.localScale.z);

        healthUiBg.rectTransform.localScale = new Vector3(healthBgXScale * index, healthUiBg.rectTransform.localScale.y,
            healthUiBg.rectTransform.localScale.z);
    }
    
    private void AnimationUpdate()
    {
        anim.SetBool("isRun", canMove);
    }

    protected abstract void AttackUpdate();
    
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
        
        targetAlly.OnDamage(enemyDetailsSo.attackPower, enemyDetailsSo.spellPower);
    }
    
    protected IEnumerator HitRoutine()
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

    public virtual void SetTarget(AllyBase ally)
    {
        if (targetAlly != null)
        {
            targetAlly.DieEvent -= DeleteTarget;
        }
        
        targetAlly = ally;

        ally.DieEvent += DeleteTarget;
        
        isTargeting = true;
        Targeting = true;
    }

    protected void DeleteTarget()
    {
        isTargeting = false;
        canMove = true;
        isAttacking = false;
        attack = false;
        targetAlly = null;
        Targeting = false;
    }

    public virtual void OnDamage(float attackPower, float spellPower) // 물리 , 마법
    {
        curHealth -= (attackPower - (attackPower * (enemyDetailsSo.defense * 0.01f))) + (spellPower - (spellPower * (enemyDetailsSo.magicResistance * 0.01f)));

        if (curHealth <= 0)
        {
            // 죽는 로직

            transform.GetComponent<Collider2D>().enabled = false;
            
            isDie = true;
            
            if (targetAlly != null)
            {
                targetAlly.DeleteTarget();
            }
            
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


        curHealth = maxHealth;
        movePosIndex = 0;
        
        moveOffset = new Vector3(0, Random.Range(-1f, 1f));

        transform.position = movePoints[movePosIndex] + moveOffset;

        canMove = true;
    }
}