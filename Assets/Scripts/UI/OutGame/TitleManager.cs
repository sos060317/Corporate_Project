using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private AudioClip titleSound;

    private void Start()
    { 
        Screen.SetResolution(1920, 1080, true);
        
        SoundManager.Instance.PlayMusic(titleSound);
    }
}