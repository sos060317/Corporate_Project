using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class FireArrowTower : MonoBehaviour
{
    public Animator FireArrowAnim;

    public AroRoundCheck ArrowRound;
    public FireArrowWindow fireArrowTowerUI;

    public GameObject RoundObject;
    public ArrowTowerTemplate arrowTemplate;
    public UpgradeArrowTower sp;
    public int alevel = 0;

    public GameObject arrowPrefab;
    public List<GameObject> spawnPositions;
    public string enemyTag = "Enemy";
    private float detectionRadius;

    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    private float timeSinceLastSpawn = 1.3f;

    public bool isClicked = false;

    public Vector2 EnemyPos;
    public bool nowShot = false;
    public bool canToggle = true;

    public AudioClip attackSound;


    private void Start()
    {
        RoundObject.SetActive(false);

        if (ArrowRound != null)
        {
            detectionRadius = ArrowRound.AroRound;
        }

    }

    private void Update()
    {
        if (GameManager.Instance.isGameStop)
        {
            FireArrowAnim.StartPlayback();
            return;
        }
        else
        {
            FireArrowAnim.StopPlayback();
        }

        alevel = sp.Arrowlevel;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(ArrowRound.transform.position, ArrowRound.AroRound);

        float radius = arrowTemplate.aweapon[alevel].Aradius;
        RoundObject.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(enemyTag) && !enemyList.Contains(collider.gameObject))
            {
                enemyList.Add(collider.gameObject);
            }
        }

        enemyList.RemoveAll(enemy => enemy == null || !IsWithinRadius(enemy.transform.position) || !enemy.activeSelf);


        nowShot = enemyList.Count > 0;

        if (nowShot)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= arrowTemplate.aweapon[alevel].AttackSpeed)
            {
                timeSinceLastSpawn = 0f;
                UpdateFlowingObjectPosition();
                //FireArrowAnim.SetBool("ItShot", true);
                StartCoroutine(AttackAnimation());
            }
        }
        else
        {
            StartCoroutine(StopAnimation());
        }

        if (fireArrowTowerUI.buttonDown == true)
        {
            isClicked = false;
            RoundObject.SetActive(false);
            fireArrowTowerUI.buttonDown = false;
        }

        StartCoroutine(DelayTm());

        if (isClicked == true && canToggle == true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    isClicked = false;
                    RoundObject.SetActive(false);
                    canToggle = false;
                }
            }
        }

        if (enemyList.Count > 0)
        {
            GameObject firstEnemy = enemyList[0];
            EnemyPos = firstEnemy.transform.position;
            nowShot = true;

        }
        else
        {
            nowShot = false;
            EnemyPos = Vector2.zero;
        }


        if (alevel == 3)
        {
            detectionRadius = 9f;
        }
    }

    private IEnumerator AttackAnimation()
    {
        FireArrowAnim.SetBool("ItShot", true);
        yield return new WaitForSeconds(0.1f);
        FireArrowAnim.SetBool("ItShot", false);
    }

    private IEnumerator StopAnimation()
    {
        FireArrowAnim.SetBool("AnimationChack", true);
        yield return new WaitForSeconds(0.2f);
        FireArrowAnim.SetBool("AnimationChack", false);
    }

    private IEnumerator DelayTm()
    {
        yield return new WaitForSeconds(0.2f);
        canToggle = true;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!isClicked && canToggle == true)
        {
            isClicked = true;
            RoundObject.SetActive(true);
            canToggle = false;
        }
    }

    private void UpdateFlowingObjectPosition()
    {
        if (enemyList.Count > 0)
        {
            GameObject firstEnemy = enemyList[0];
            Vector2 targetPosition = firstEnemy.transform.position;
        }
        else
        {
            FireArrowAnim.SetBool("ItShot", false);
        }
    }

    private void SpawnArrowsAtPositions()
    {
        SoundManager.Instance.PlaySound(attackSound);

        foreach (GameObject spawnPosition in spawnPositions)
        {
            if (spawnPosition.activeSelf)
            {
                var temp = Instantiate(arrowPrefab, spawnPosition.transform.position, Quaternion.identity).GetComponent<FireArrow>();

                temp.fireArrowTower = GetComponent<FireArrowTower>();
            }
        }
    }

    private bool IsWithinRadius(Vector2 position)
    {
        return Vector2.Distance(ArrowRound.transform.position, position) <= ArrowRound.AroRound;
    }

    private void OnDrawGizmosSelected()
    {

        if (ArrowRound != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(ArrowRound.transform.position, ArrowRound.AroRound);

            //Gizmos.color = Color.green;
            //Gizmos.DrawWireSphere(ArrowRound.transform.position, detectionRadius);

        }
    }
}
