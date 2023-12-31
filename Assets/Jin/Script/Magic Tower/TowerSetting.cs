using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerSetting : MonoBehaviour
{
    public MagicRoundCheck magicRoundCheck;

    public GameObject MagicPrefab;
    public List<GameObject> spawnPositions = new List<GameObject>();
    public string enemyTag = "Enemy";
    public float detectionRadius = 5.0f;

    public List<GameObject> enemyList = new List<GameObject>();
    private bool canSpawn = false;
    private float spawnInterval = 1.5f;
    private float timeSinceLastSpawn = 1f;


    public Vector2 MEnemyPos;
    public bool itTimeToShot = false;

    public Animator MagicTowerAnim;

    private void Start()
    {
        if(magicRoundCheck != null)
        {
            detectionRadius = magicRoundCheck.MgcRound;
        }
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(magicRoundCheck.transform.position, magicRoundCheck.MgcRound);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(enemyTag) && !enemyList.Contains(collider.gameObject))
            {
                enemyList.Add(collider.gameObject);
                
            }
        }

        enemyList.RemoveAll(enemy => enemy == null || !IsWithinRadius(enemy.transform.position) || !enemy.activeSelf);

        

        canSpawn = enemyList.Count > 0;

        if (canSpawn)
        {
            

            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                timeSinceLastSpawn = 0f;
                foreach (GameObject spawnPos in spawnPositions)
                {
                    if(spawnPos.activeSelf)
                    {
                        StartCoroutine(AttackAnimation());
                        SpawnMagic(spawnPos);
                        
                        UpdateFlowingObjectPosition();
                        //SpawnMagicsAtPositions();
                    }
                }
            }
        }
        else
        {

        }

        if (enemyList.Count > 0) //FlowingObject != null && 
        {
            Vector2 targetPosition = enemyList[0].transform.position;
            MEnemyPos = targetPosition;
            itTimeToShot = true;
        }
        else
        {
            itTimeToShot = false;
            MEnemyPos = Vector2.zero;
        }
    }

    private IEnumerator AttackAnimation()
    {
        MagicTowerAnim.SetBool("ItShot", true);
        yield return new WaitForSeconds(0.1f);
        MagicTowerAnim.SetBool("ItShot", false);
    }

    private void SpawnMagic(GameObject spawnPosition)
    {
        //var temp = Instantiate(MagicPrefab, spawnPosition.transform.position, Quaternion.identity).GetComponent<ThrowAndDamage>();

        //temp.MagicTower = GetComponent<TowerSetting>();

        GameObject magicInstance = Instantiate(MagicPrefab, spawnPosition.transform.position, Quaternion.identity);
        ThrowAndDamage throwAndDamage = magicInstance.GetComponent<ThrowAndDamage>();

        if (throwAndDamage != null)
        {
            Vector2 direction = (enemyList.Count > 0) ? (enemyList[0].transform.position - spawnPosition.transform.position).normalized : Vector2.zero;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            magicInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            throwAndDamage.MagicTower = GetComponent<TowerSetting>();
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

    private bool IsWithinRadius(Vector2 position)
    {
        return Vector2.Distance(magicRoundCheck.transform.position, position) <= detectionRadius;
    }

    private void OnDrawGizmosSelected()
    {
        if(magicRoundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(magicRoundCheck.transform.position, magicRoundCheck.MgcRound);
        }
        
    }
}
