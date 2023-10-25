using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private static WaveManager instance = null;

    public static WaveManager Instance
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

    [HideInInspector] public int enemySpawnerCount;
    
    public Action waveStartEvent;
    public Action waveEndEvent;

    private int waveCompleteCount;
    private int waveEndCount;
    public int enemyCount;

    public bool stageClear = false;

    private WaitForSeconds nextWaveDelay;

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

    private void Start()
    {
        nextWaveDelay = new WaitForSeconds(2f);

        waveCompleteCount = 0;
    }

    public void WaveComplete()
    {
        waveCompleteCount++;
        
        if (waveCompleteCount >= enemySpawnerCount)
        {
            Debug.Log("Next Wave.");

            StartCoroutine(EndWaveRoutine());
            
            waveCompleteCount = 0;
        }
    }

    public void WaveEnd()
    {
        waveEndCount++;

        if (waveEndCount >= enemySpawnerCount)
        {
            Debug.Log("Stage Clear!");

            stageClear = true;
        }
    }
    
    public void EnemyCountPlus()
    {
        enemyCount++;
    }
    
    public void EnemyCountMinus()
    {
        enemyCount--;

        if (enemyCount <= 0 && stageClear)
        {
            GameManager.Instance.GameClear();
        }
    }
    
    public void WaveStart()
    {
        waveStartEvent?.Invoke();
        
        GameManager.Instance.NextWave();
    }
    
    private IEnumerator EndWaveRoutine()
    {
        yield return nextWaveDelay;
        
        waveEndEvent?.Invoke();
    }
}