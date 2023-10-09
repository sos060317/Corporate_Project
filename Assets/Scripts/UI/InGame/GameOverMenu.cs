using System;
using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TransitionSettings transitionSettings;

    public void LoadScene(string sceneName)
    {
        TransitionManager.Instance().Transition(sceneName, transitionSettings, 0);
    }
}