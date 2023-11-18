using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectionPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingMenu;
    [SerializeField] private GameObject developerMenu;
    [SerializeField] private GameObject titleRequestionMenu;
    [SerializeField] private GameObject exitRequestionMenu;
    [SerializeField] private GameObject resetRequestionMenu;

    private bool isShow = false;

    private void Start()
    {
        mainMenu.SetActive(false);
        settingMenu.SetActive(false);
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
            HideSettingMenu();
            HideDeveloper();
            HideTitleRequestion();
            HideExitRequestion();
            HideResetRequestion();
        }
    }

    private void ShowPauseMenu()
    {
        isShow = true;

        mainMenu.SetActive(true);

        if(SceneManager.GetActiveScene().name != "StageSelection") 
        {
            GameManager.Instance.isGameStop = true;
        }
    }

    public void HidePauseMenu()
    {
        isShow = false;

        mainMenu.SetActive(false);

        if (SceneManager.GetActiveScene().name != "StageSelection")
        {
            GameManager.Instance.isGameStop = false;
        }
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("Title");
    }

    public void ShowSettingMenu()
    {
        settingMenu.SetActive(true);
    }

    public void HideSettingMenu()
    {
        settingMenu.SetActive(false);
    }

    public void ShowDeveloper()
    {
        developerMenu.SetActive(true);
    }

    public void HideDeveloper()
    {
        developerMenu.SetActive(false);
    }

    public void ShowTitleRequestion()
    {
        titleRequestionMenu.SetActive(true);
    }

    public void HideTitleRequestion()
    {
        titleRequestionMenu.SetActive(false);
    }

    public void ShowExitRequestion()
    {
        exitRequestionMenu.SetActive(true);
    }

    public void HideExitRequestion()
    {
        exitRequestionMenu.SetActive(false);
    }

    public void ShowResetRequestion()
    {
        resetRequestionMenu.SetActive(true);
    }

    public void HideResetRequestion()
    {
        resetRequestionMenu.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    
    public void ResetButton()
    {
        PlayerPrefs.DeleteAll();
    }
}
