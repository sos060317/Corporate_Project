using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text[] tipPrefabs;
    [SerializeField] private TMP_Text tipText;

    private int tipNumber = 0;

    private void Start()
    {
        tipText.text = tipPrefabs[tipNumber].text;
    }

    public void NextTip()
    {
        tipText.text = tipPrefabs[tipNumber].text;

        tipNumber++;

        if(tipNumber >= tipPrefabs.Length)
        {
            tipNumber = 0;
        }
    }
}
