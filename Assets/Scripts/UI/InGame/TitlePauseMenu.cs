using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;

    private bool isShow = false;

    private void Start()
    {
        mainMenu.SetActive(false);
    }

    private void Update()
    {
        CheckEscapeKey();
    }

    private void CheckEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isShow)
        {
            ShowPauseMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isShow)
        {
            HidePauseMenu();
        }
    }

    private void ShowPauseMenu()
    {
        isShow = true;

        mainMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        isShow = false;

        mainMenu.SetActive(false);
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("Title");
    }

    public void SettingButton()
    {
        
    }
    
    public void ExitButton()
    {

    }
    
    public void ResetButton()
    {
        
    }
}
