using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectionManager : MonoBehaviour
{
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioClip fadeInSound;

    private void Start()
    {
        SoundManager.Instance.PlayMusic(bgm);
        SoundManager.Instance.PlaySound(fadeInSound);
    }
}