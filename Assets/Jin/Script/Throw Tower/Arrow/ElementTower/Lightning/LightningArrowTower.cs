using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class LightningArrowTower : MonoBehaviour
{
    public Animator LightningArrowAnim;

    public AroRoundCheck ArrowRound;
    public LightningArrowWindow LightningArrowTowerUI;

    public GameObject RoundObject;
    public ArrowTowerTemplate arrowTemplate;
    public UpgradeArrowTower sp;
    public int alevel = 0;

    public GameObject arrowPrefab;
    public List<GameObject> spawnPositions;
    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;

    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    private float timeSinceLastSpawn = 1f;

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

        //Debug.Log(enemyList.Count); ?? ?? ???

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
                //SpawnArrowsAtPositions();

                StartCoroutine(AttackAnimation());
            }
        }
        else
        {

        }

        if (LightningArrowTowerUI.buttonDown == true)
        {
            isClicked = false;
            RoundObject.SetActive(false);
            LightningArrowTowerUI.buttonDown = false;
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
        LightningArrowAnim.SetBool("ItShot", true);
        yield return new WaitForSeconds(0.1f);
        LightningArrowAnim.SetBool("ItShot", false);
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
    }

    private void SpawnArrowsAtPositions()
    {
        SoundManager.Instance.PlaySound(attackSound);
        
        foreach (GameObject spawnPosition in spawnPositions)
        {
            if (spawnPosition.activeSelf)
            {
                var temp = Instantiate(arrowPrefab, spawnPosition.transform.position, Quaternion.identity).GetComponent<LightningArrow>();

                temp.lightningArrowTower = GetComponent<LightningArrowTower>();
            }
        }
    }

    private bool IsWithinRadius(Vector2 position)
    {
        return Vector2.Distance(ArrowRound.transform.position, position) <= detectionRadius;  //  ???? ??? Enmey ??? ????
    }

    private void OnDrawGizmosSelected()
    {

        if (ArrowRound != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(ArrowRound.transform.position, ArrowRound.AroRound);
        }
    }
}
