using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionUpgradeMenu : MonoBehaviour
{
    [SerializeField] private EvolutionStoneButton[] upgradeButtons;

    private void Start()
    {
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            upgradeButtons[i].Init();
        }
    }
}