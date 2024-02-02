using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class GoldTower : MonoBehaviour
{
    public bool isClick;
    public GameObject goldBananaPrefab;
    public Vector2 spawnRange = new Vector2(3.5f, 1.5f);
    public float spawnRangeY = 1f;

    [SerializeField]
    private bool isSpawn;

    [SerializeField]
    private float spawnTime = 1f;
    private float timer = 0f;

    [SerializeField]
    private List<GameObject> spawnedBananas = new List<GameObject>();

    private void Start()
    {
        isSpawn = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.CompareTag("GoldTower") && !EventSystem.current.IsPointerOverGameObject())
                {
                    StartCoroutine(SetIsClickTrueForDuration(.5f));

                    foreach (GameObject banana in spawnedBananas)
                    {
                        if (banana != null)
                        {
                            GoldBanana goldBananaScript = banana.GetComponent<GoldBanana>();
                            if (goldBananaScript != null)
                            {
                                goldBananaScript.GetGold();
                            }
                        }
                    }

                    break;
                }
            }
        }

        if (isSpawn)
        {
            SpawnBanana();
        }
        CheckDestroyedBananas();
    }

    private void SpawnBanana()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            float randomX = Random.Range(transform.position.x - spawnRange.x / 2f, transform.position.x + spawnRange.x / 2f);

            float randomY = Random.Range(transform.position.y - spawnRange.y / 2f, transform.position.y + spawnRange.y / 2f);

            randomX = Mathf.Clamp(randomX, transform.position.x - spawnRange.x / 2f, transform.position.x + spawnRange.x / 2f);
            randomY = Mathf.Clamp(randomY, transform.position.y - spawnRange.y / 2f, transform.position.y + spawnRange.y / 2f);

            randomY += spawnRangeY / 2;

            Vector2 randomPosition = new Vector2(randomX, randomY);

            GameObject newBanana = Instantiate(goldBananaPrefab, randomPosition, Quaternion.identity);
            spawnedBananas.Add(newBanana);

            timer = 0f;
        }
    }

    private void CheckDestroyedBananas()
    {
        spawnedBananas.RemoveAll(banana => banana == null || banana.GetComponent<GoldBanana>() == null);
    }

    private System.Collections.IEnumerator SetIsClickTrueForDuration(float duration)
    {
        isClick = true;
        yield return new WaitForSeconds(duration);
        isClick = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        float offsetY = spawnRangeY / 2f;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z), new Vector3(spawnRange.x, spawnRange.y, 0f));
    }
}
