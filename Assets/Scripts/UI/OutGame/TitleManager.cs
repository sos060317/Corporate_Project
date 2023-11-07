using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private AudioClip titleSound;

    private void Start()
    {
        SoundManager.Instance.PlayMusic(titleSound);
    }
}