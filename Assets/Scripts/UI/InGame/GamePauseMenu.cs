using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class GamePauseMenu : MonoBehaviour
{
    [SerializeField] private TransitionSettings transition;
    
    public void CloseMenu()
    {
        GameManager.Instance.isGameStop = false;

        gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        TransitionManager.Instance().Transition(sceneName, transition, 0f);
    }
}