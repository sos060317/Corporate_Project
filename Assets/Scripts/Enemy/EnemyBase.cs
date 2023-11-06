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
    [SerializeField] private Material iceMaterial;
    [SerializeField] private Image healthUiBg;
    [SerializeField] private Image healthUiBar;

    [HideInInspector] public bool Targeting = false;
    
    private int movePosIndex;
    
    
    private float baseSpeed;

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
    [HideInInspector] public float defense;
    [HideInInspector] public float magicResistance;
    [HideInInspector] public float attackPower;
    [HideInInspector] public float spellPower;
    [HideInInspector] public float moveSpeed;
    protected float attackRate;
    protected float attackTimer;
    protected float attackRange;
    
    [HideInInspector] public EnemyDetailsSO enemyDetailsSo;
    
    protected Animator anim;

    protected AllyBase targetAlly;

    private bool burningNow = false; // 타고 있는지 확인
    private Coroutine burningCoroutine;
    //private float burningTimeLeft;

    private bool PoisoingNow = false;
    private Coroutine PoisoingCoroutine;

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
            transform.position += dir.normalized * (moveSpeed * Time.deltaTime);
            
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
        transform.position += nextPos.normalized * (moveSpeed * Time.deltaTime);
        
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
                attackPower * GameManager.Instance.enemyAttackDamageMultiply,
                spellPower * GameManager.Instance.enemyAttackDamageMultiply);
            return;
        }
        
        if (targetAlly == null)
        {
            return;
        }

        targetAlly.OnDamage(attackPower * GameManager.Instance.enemyAttackDamageMultiply,
            spellPower * GameManager.Instance.enemyAttackDamageMultiply);
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
                curHealth - ((attackPower - (attackPower * (defense * 0.01f))) +
                (spellPower - (spellPower * (magicResistance * 0.01f)))), 0);

        if (curHealth <= 0)
        {
            // 죽는 로직

            GameManager.Instance.GetGold(enemyDetailsSo.coins * StageEnemyCharacteristicsManager.Instance.coin);
            
            transform.GetComponent<Collider2D>().enabled = false;
            
            isDie = true;
            canMove = false;
            
            if (targetAlly != null)
            {
                targetAlly.DeleteTarget();
                targetAlly.EnemyUnTargetingEvent -= DeleteTarget;
            }
            
            anim.SetTrigger("Die");
            //return;
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

        // 기본 스탯 초기화
        maxHealth = this.enemyDetailsSo.enemyBaseHealth * StageEnemyCharacteristicsManager.Instance.health;
        moveSpeed = this.enemyDetailsSo.enemyBaseMoveSpeed * StageEnemyCharacteristicsManager.Instance.moveSpeed;
        baseSpeed = moveSpeed;
        attackRate = this.enemyDetailsSo.enemyBaseAttackRate * StageEnemyCharacteristicsManager.Instance.attackRate;
        attackRange = this.enemyDetailsSo.enemyBaseAttackRange;
        defense = this.enemyDetailsSo.defense + StageEnemyCharacteristicsManager.Instance.defnse;
        magicResistance = this.enemyDetailsSo.magicResistance + StageEnemyCharacteristicsManager.Instance.magicResistance;
        attackPower = this.enemyDetailsSo.attackPower * StageEnemyCharacteristicsManager.Instance.attackPower;
        spellPower = this.enemyDetailsSo.spellPower * StageEnemyCharacteristicsManager.Instance.spellPower;
        
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


    //화염
    public void FireEnemy(float fireTime, float attackPower, int repetitions)
    {
        if (!burningNow)
        {
            burningCoroutine = StartCoroutine(BurningEffect(fireTime, attackPower, repetitions));
        }
        else if(isDie)
        {
            StopCoroutine(burningCoroutine);
            burningNow = false;
            burningCoroutine = null;
        }
        else
        {
            // 코루틴 재설정
            StopCoroutine(burningCoroutine);
            burningNow = false;
            burningCoroutine = null;

            // 코루틴 다시 시작하기
            burningCoroutine = StartCoroutine(BurningEffect(fireTime, attackPower, repetitions));
        }
    }

    private IEnumerator BurningEffect(float fireTime, float attackPower, int repetitions)
    {
        burningNow = true; // bool burningNow값 true로 바꾸기

        for (int count = 0; count < repetitions; count++)
        {
            // 마법 데미지 0으로 넣기
            float spellPower = 0.0f;

            OnDamage(attackPower, spellPower); // 데미지
            //Debug.Log(spellPower);

            //yield return new WaitForSeconds(1.0f); // 1초 기다리기

            yield return new WaitForSeconds(fireTime / repetitions);
        }

        yield return new WaitForSeconds(fireTime - repetitions);

        // 코루틴 재설정후 중지
        burningNow = false;
        burningCoroutine = null;
    }


    // 독
    public void PoisonEnemy(float PoisonTime, float attackPower, int repetitions)
    {
        if (!PoisoingNow)
        {
            PoisoingCoroutine = StartCoroutine(PoisoningEffect(PoisonTime, attackPower, repetitions));
        }
        else if (curHealth <= 0)
        {
            Debug.Log("응애");
            StopCoroutine(PoisoingCoroutine);
            PoisoingNow = false;
        PoisoingCoroutine = null;
        }
        else
        {
            StopCoroutine(PoisoingCoroutine);
            PoisoingNow = false;
            PoisoingCoroutine = null;

            PoisoingCoroutine = StartCoroutine(PoisoningEffect(PoisonTime, attackPower, repetitions));
        }
    }

    private IEnumerator PoisoningEffect(float PoisonTime, float spellPower, int repetitions)
    {
        PoisoingNow = true;

        for (int count = 0; count < repetitions; count++)
        {
            float attackPower = 0.0f;

            OnDamage(attackPower, spellPower);

            yield return new WaitForSeconds(PoisonTime / repetitions);
        }

        yield return new WaitForSeconds(PoisonTime - repetitions);

        PoisoingNow = false;
        PoisoingCoroutine = null;
    }


    // 기절?
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

    private Coroutine speedDownCoroutine;
    

    // 슬로우?
    public void SpeedDownEnemy(float speedDownTime)
    {
        if (speedDownCoroutine != null)
        {
            StopCoroutine(speedDownCoroutine);
        }
        
        speedDownCoroutine = StartCoroutine(SpeedDownRoutine(speedDownTime));
    }

    private IEnumerator SpeedDownRoutine(float speedDownTime)
    {
        // 색상 변경
        sr.material = iceMaterial;
        
        // 애니메이션 스피드 감소
        anim.SetFloat("animSpeed", 0.7f);
        
        // 이동속도 30% 감소
        moveSpeed = baseSpeed * 0.7f;

        yield return new WaitForSeconds(speedDownTime);
        
        anim.SetFloat("animSpeed", 1f);

        sr.material = defaultMaterial;

        moveSpeed = baseSpeed;
    }
}