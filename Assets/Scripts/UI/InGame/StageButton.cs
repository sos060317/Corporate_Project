using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField] private Image panel;
    [SerializeField] private int levelIndex;
    
    private int currentStarsNum = 0;

    private bool isEscape;

    private void Update()
    {
        CheckPressEscape();
    }

    public void PressStarsButton(int starsNum)
    {
        currentStarsNum = starsNum;

        if (currentStarsNum > PlayerPrefs.GetInt("Lv" + levelIndex))
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, starsNum);
        }
        
        Debug.Log(PlayerPrefs.GetInt("Lv" + levelIndex, starsNum));
    }
    
    private void CheckPressEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isEscape)
            {
                isEscape = false;
                panel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                isEscape = true;
                panel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    
    public void RestartButton()
    {
        panel.gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(gameObject.scene.name);
    }
    
    public void ExitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StageSelection");
    }
    
    public void ContinueButton()
    {
        Time.timeScale = 1;
        panel.gameObject.SetActive(false);
    }
}
