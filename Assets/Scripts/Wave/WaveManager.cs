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

    public int enemySpawnerCount;
    
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
}