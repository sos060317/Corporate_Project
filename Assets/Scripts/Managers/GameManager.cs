using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public int defianceLife;
    
    [SerializeField] private Text defianceLifeText;
    [SerializeField] private EnemySpawner enemySpawner;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        DefianceLifeUpdate();
    }

    private void DefianceLifeUpdate()
    {
        defianceLifeText.text = "Life: " + defianceLife.ToString();

        if (defianceLife <= 0)
        {
            enemySpawner.enabled = false;
        }
    }
}
