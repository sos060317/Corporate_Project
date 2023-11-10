using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSilder : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private bool isMute;

    private float beforeVolume;

    private void Start()
    {
        if (SoundManager.Instance.bgmVolume == 0)
        {
            isMute = true;
        }
        else
        {
            isMute = false;
        }
        
        volumeSlider.value = SoundManager.Instance.sfxVolume;
        volumeSlider.onValueChanged.AddListener(val => SoundManager.Instance.SetEffectVolume(val));
    }

    private void Update()
    {
        if (SoundManager.Instance.sfxVolume == 0)
        {
            isMute = true;
        }
        else
        {
            isMute = false;
        }
    }
}