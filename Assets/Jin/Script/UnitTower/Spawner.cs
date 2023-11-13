using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public TextMeshProUGUI UpgradeCostText;
    public TextMeshProUGUI DestroyCost;

    public TowerTemplate towerTemplate;

    //private GameManager playerGold;
    private int level = 0;
    private bool isSpawning = false;

    private List<GameObject> spawnedUnits = new List<GameObject>(); // ��ȯ�� ����(Unit)�� ������ ����Ʈ

    public List<GameObject> prefabsToSpawn; // ��ȯ ������ Prefab�� ���� �� �ִ� List
    public Transform parentTransform; // �θ�� ������ Transform
    public GameObject Round;

    private bool SpawncostCheck = false;

    public int StartingCoin;


    private void Start()
    {
        //playerGold = FindObjectOfType<GameManager>();

        // ������ �� MaxUnit ����ŭ ��ȯ
        int initialUnitsToSpawn = towerTemplate.weapon[level].MaxUnit;
        SpawnUnits(initialUnitsToSpawn);

        // Testcoin ����ŭ PlayerGold�� ó�� �� ���� ���ҽ�Ŵ
        if (!SpawncostCheck)
        {
            GameManager.Instance.UseGold(towerTemplate.weapon[level].cost);
            SpawncostCheck = true;
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStop)
        {
            return;
        }

        UpdateCostText();
        DestroyCostText();

        // Ȱ��ȭ ���°� �ƴ� ����(Unit)�� ����Ʈ���� ����
        spawnedUnits.RemoveAll(unit => unit == null || !unit.activeSelf);

        if (!isSpawning && spawnedUnits.Count < towerTemplate.weapon[level].MaxUnit)
        {
            StartCoroutine(SpawnUnitsWithDelay(towerTemplate.weapon[level].SpawnTime));
        }

        float radius = towerTemplate.weapon[level].radius;
        Round.transform.localScale = new Vector3(radius, radius, radius);
    }

    private IEnumerator SpawnUnitsWithDelay(float delay)
    {
        isSpawning = true;

        // delay �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        int unitsToSpawn = towerTemplate.weapon[level].MaxUnit - spawnedUnits.Count;

        for (int i = 0; i < unitsToSpawn; i++)
        {
            // ��ȯ ������ Prefab�� ���� �����ִ��� Ȯ��
            if (prefabsToSpawn.Count > 0)
            {
                GameObject prefabToSpawn = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)];
                SpawnPrefab(prefabToSpawn);
            }
            else
            {
                // �� �̻� ��ȯ�� �� �ִ� Prefab�� ������ ����
                break;
            }
        }

        isSpawning = false;
    }

    private void SpawnUnits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // ��ȯ ������ Prefab�� ���� �����ִ��� Ȯ��
            if (prefabsToSpawn.Count > 0)
            {
                GameObject prefabToSpawn = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)];
                SpawnPrefab(prefabToSpawn);
            }
            else
            {
                // �� �̻� ��ȯ�� �� �ִ� Prefab�� ������ ����
                break;
            }
        }
    }

    public void DestroyTower()
    {
        Destroy(gameObject);
        GameManager.Instance.GetGold(towerTemplate.weapon[level].ResellCost);
    }

    private void UpdateCostText()
    {
        // costText�� null�� �ƴ϶�� TMP �ؽ�Ʈ ������Ʈ
        if (UpgradeCostText != null)
        {
            UpgradeCostText.text = towerTemplate.weapon[level + 1].cost.ToString();
        }
    }

    private void DestroyCostText()
    {
        // costText�� null�� �ƴ϶�� TMP �ؽ�Ʈ ������Ʈ
        if (DestroyCost != null)
        {
            DestroyCost.text = towerTemplate.weapon[level].ResellCost.ToString();
        }
    }

    private void SpawnPrefab(GameObject prefab)
    {
        GameObject spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);

        if (parentTransform != null)
        {
            spawnedObject.transform.SetParent(parentTransform);
        }

        spawnedUnits.Add(spawnedObject);
    }

    // ����(Unit)�� ����� �� ȣ���� �޼���
    public void UnitDestroyed(GameObject unit)
    {
        if (spawnedUnits.Contains(unit))
        {
            spawnedUnits.Remove(unit);
        }
    }

    // ���׷��̵� �޼���
    public void UpgradeTower()
    {
        if (level < towerTemplate.weapon.Length - 1)
        {
            if (GameManager.Instance.currentGold >= towerTemplate.weapon[level + 1].cost)
            {
                float spcost = towerTemplate.weapon[level + 1].cost;
                GameManager.Instance.UseGold(spcost);

                level++;
                Debug.Log("Ÿ�� ���׷��̵�: ���� " + level);
            }
            else
            {
                Debug.Log("�� �����ϴ�");
            }
        }
        else
        {
            Debug.Log("�ִ� ���׷��̵� ���� �Դϴ�.");
        }
    }
}
