using System.Collections;
using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class GamePauseMenu : MonoBehaviour
{
    [SerializeField] private TransitionSettings transition;
    [SerializeField] private AudioClip fadeOutSound;
    
    public void CloseMenu()
    {
        GameManager.Instance.isGameStop = false;

        gameObject.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        SoundManager.Instance.PlaySound(fadeOutSound);
        TransitionManager.Instance().Transition(sceneName, transition, 0f);
    }
}