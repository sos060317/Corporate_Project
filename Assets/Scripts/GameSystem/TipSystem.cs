using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text[] tipPrefabs;
    [SerializeField] private TMP_Text tipText;
    [SerializeField] private AudioClip bookSound;

    private int tipNumber = 0;

    private void Start()
    {
        tipText.text = tipPrefabs[tipNumber].text;
    }

    public void NextTip()
    {
        SoundManager.Instance.PlaySound(bookSound, Random.Range(0.7f, 1.3f));
        
        tipNumber++;

        if(tipNumber >= tipPrefabs.Length)
        {
            tipNumber = 0;
        }

        tipText.text = tipPrefabs[tipNumber].text;
    }
}
