using System;
using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TransitionSettings transitionSettings;
    [SerializeField] private AudioClip fadeOutSound;

    public void LoadScene(string sceneName)
    {
        SoundManager.Instance.PlaySound(fadeOutSound);
        TransitionManager.Instance().Transition(sceneName, transitionSettings, 0);
    }
}