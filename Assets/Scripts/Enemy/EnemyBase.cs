using System;
using System.Collections;
using System.Collections.Generic;
//qusing UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Image healthUiBg;
    [SerializeField] private Image healthUiBar;

    [HideInInspector] public bool Targeting = false;
    
    private int movePosIndex;
    
    private float moveSpeed;

    private bool isFaint = false;
    
    protected bool canMove = true;
    protected bool isTargeting = false;
    protected bool isAttacking = false;
    protected bool isDie = false;
    protected bool attack = false;
    protected bool isMoveEnd = false;
    
    private Vector2[] movePoints;
    private Vector2 moveOffset;

    private SpriteRenderer sr;
    private WaitForSeconds hitDelay;
    
    [HideInInspector] public float maxHealth;
    [HideInInspector] public float curHealth;
    protected float attackRate;
    protected float attackTimer;
    protected float attackRange;
    
    [HideInInspector] public EnemyDetailsSO enemyDetailsSo;
    
    protected Animator anim;

    protected AllyBase targetAlly;

    protected virtual void Start()
    {
        // 컴포넌트 할당
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        // 변수 초기화
        hitDelay = new WaitForSeconds(0.1f);
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.isGameStop)
        {
            anim.StartPlayback();
            return;
        }
        else
        {
            anim.StopPlayback();
        }

        if (isFaint)
        {
            return;
        }
        
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
            transform.position += dir.normalized * (moveSpeed * Time.deltaTime * GameManager.Instance.enemyMoveSpeedMultiply);
            
            if (dir.normalized.x < 0)
            {
                FlipFunction(true);
            }
            else
            {
                FlipFunction(false);
            }

            if (Vector2.Distance(transform.position, targetAlly.transform.position ) <= attackRange)
            {
                canMove = false;
                isAttacking = true;
            }
            
            return;
        }
        
        Vector3 nextPos = (Vector3)movePoints[movePosIndex] - transform.position + (Vector3)moveOffset;
        transform.position += nextPos.normalized * (moveSpeed * Time.deltaTime * GameManager.Instance.enemyMoveSpeedMultiply);
        
        if (nextPos.normalized.x < 0)
        {
            FlipFunction(true);
        }
        else if (nextPos.normalized.x > 0)
        {
            FlipFunction(false);
        }

        if (Vector2.Distance(transform.position - (Vector3)(moveOffset), movePoints[movePosIndex]) <= 0.3f)
        {
            movePosIndex++;
        }

        if (movePosIndex >= movePoints.Length)
        {
            //GameManager.Instance.defianceLife--;
            isMoveEnd = true;
            canMove = false;
            isAttacking = true;
        }
    }

    private void FlipFunction(bool index)
    {
        sr.flipX = index;
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
        if (isMoveEnd)
        {
            GameManager.Instance.OnEvolutionStoneDamaged(
                enemyDetailsSo.attackPower * GameManager.Instance.enemyAttackDamageMultiply,
                enemyDetailsSo.spellPower * GameManager.Instance.enemyAttackDamageMultiply);
            return;
        }
        
        if (targetAlly == null)
        {
            return;
        }

        targetAlly.OnDamage(enemyDetailsSo.attackPower * GameManager.Instance.enemyAttackDamageMultiply,
            enemyDetailsSo.spellPower * GameManager.Instance.enemyAttackDamageMultiply);
    }
    
    protected IEnumerator HitRoutine()
    {
        sr.material = hitMaterial;

        yield return hitDelay;

        sr.material = defaultMaterial;
    }
    
    // 적이 죽는 애니메이션을 다 실행하고 호출되는 애니메이션 이벤트 함수
    private void SetActiveFalse()
    {
        WaveManager.Instance.EnemyCountMinus();
        
        gameObject.SetActive(false);
    }

    public virtual void SetTarget(AllyBase ally)
    {
        if (isMoveEnd)
        {
            return;
        }
        
        if (targetAlly != null)
        {
            targetAlly.EnemyUnTargetingEvent -= DeleteTarget;
        }
        
        targetAlly = ally;

        targetAlly.EnemyUnTargetingEvent += DeleteTarget;
        
        isTargeting = true;
        Targeting = true;

        if (isDie)
        {
            targetAlly.DeleteTarget();
            targetAlly.EnemyUnTargetingEvent -= DeleteTarget;
        }
    }

    protected void DeleteTarget()
    {
        targetAlly.EnemyUnTargetingEvent -= DeleteTarget;
        
        isTargeting = false;
        canMove = true;
        isAttacking = false;
        attack = false;
        targetAlly = null;
        Targeting = false;
    }

    public virtual void OnDamage(float attackPower, float spellPower) // 물리 , 마법
    {
        curHealth =
            Mathf.Max(
                curHealth - ((attackPower - (attackPower * (enemyDetailsSo.defense * 0.01f))) +
                (spellPower - (spellPower * (enemyDetailsSo.magicResistance * 0.01f)))), 0);

        if (curHealth <= 0)
        {
            // 죽는 로직

            GameManager.Instance.GetGold(enemyDetailsSo.coins);
            
            transform.GetComponent<Collider2D>().enabled = false;
            
            isDie = true;
            canMove = false;
            
            if (targetAlly != null)
            {
                targetAlly.DeleteTarget();
                targetAlly.EnemyUnTargetingEvent -= DeleteTarget;
            }
            
            anim.SetTrigger("Die");
            return;
        }
        
        StartCoroutine(HitRoutine());
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        HUDManager.Instance.ShowEnemyWindow(this);
    }

    public void InitEnemy(Vector2[] movePoints, EnemyDetailsSO enemyDetailsSo)
    {
        this.movePoints = movePoints;
        this.enemyDetailsSo = enemyDetailsSo;

        maxHealth = this.enemyDetailsSo.enemyBaseHealth;
        moveSpeed = this.enemyDetailsSo.enemyBaseMoveSpeed;
        attackRate = this.enemyDetailsSo.enemyBaseAttackRate;
        attackRange = this.enemyDetailsSo.enemyBaseAttackRange;
        
        WaveManager.Instance.EnemyCountPlus();

        curHealth = maxHealth;
        movePosIndex = 0;

        moveOffset = Random.insideUnitCircle * 0.7f;

        transform.position = movePoints[movePosIndex] + moveOffset;

        canMove = true;
        
        // 모든 변수 초기화
        attackTimer = 0f;
        isDie = false;
        Targeting = false;
        isTargeting = false;
        targetAlly = null;
        isAttacking = false;
        attack = false;
        isMoveEnd = false;
        Targeting = false;
        isFaint = false;
        transform.GetComponent<Collider2D>().enabled = true;
    }

    public void FaintEnemy(float faintTime)
    {
        StartCoroutine(FaintRoutine(faintTime));
    }

    private IEnumerator FaintRoutine(float faintTime)
    {
        isFaint = true;
        
        anim.SetBool("Faint", true);

        yield return new WaitForSeconds(faintTime);

        isFaint = false;
        
        anim.SetBool("Faint", false);
    }
}