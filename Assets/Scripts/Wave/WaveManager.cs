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
    
    public Action waveEvent;

    private int waveCompleteCount = 0;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WaveStart();
        }
    }

    public void WaveComplete()
    {
        waveCompleteCount++;

        if (waveCompleteCount >= enemySpawnerCount)
        {
            Debug.Log("Next Wave.");
            
            waveEvent?.Invoke();
            waveCompleteCount = 0;
        }
    }
    
    public void WaveStart()
    {
        waveEvent?.Invoke();
        waveCompleteCount = 0;
    }
}