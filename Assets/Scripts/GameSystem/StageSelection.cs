using System;
using UnityEngine;
using UnityEngine.UI;

// 선택한 스테이지로 이동
public class StageSelection : MonoBehaviour
{
    [Header("스테이지")]
    [SerializeField] private bool unlocked;      // 스테이지가 풀렸는지 아닌지를 판별하는 변수
    [SerializeField] private Image unlockImage;  // 스테이지가 잠겼는지 알려주는 이미지
    [SerializeField] private GameObject[] stars; // 스테이지의 클리어 정도를 알려주는 별
    //[SerializeField] private Sprite starSprite;  // 스테이지의 클리어 정도를 알려주는 별의 그림
    
    [Header("씬 로딩")]
    [SerializeField] private SceneLoader sceneLoader;

    private void Update()
    {
        UpdateLevelImage();
        UpdateLevelStatus();
    }

    public void ResetPlayerPrefab()
    {
        PlayerPrefs.DeleteAll();
    }

    private void UpdateLevelStatus()
    {
        int previousLevelNum = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("Lv" + previousLevelNum) > 0)
        {
            unlocked = true;
        }
    }

    // 스테이지 클리어 여부를 알려주는 함수
    private void UpdateLevelImage()
    {
        if (!unlocked) // 만약 스테이지를 클리어 하지 못헸다면
        {
            unlockImage.gameObject.SetActive(true); // 잠긴 이미지를 보여주고
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(false); // 별 이미지를 없애준다
            }
        }
        else // 스테이지를 클리어 했다면
        {
            unlockImage.gameObject.SetActive(false); // 잠긴 이미지를 없애주고
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(true); // 별 이미지를 보여준다
            }

            for (int i = 0; i < PlayerPrefs.GetInt("Lv" + gameObject.name); i++)
            {
                stars[i].gameObject.GetComponent<Image>().color = Color.yellow;
            }
        }
    }

    // 스테이지를 선택했을 때의 씬 이동
    public void PressSelection(int stageId)
    {
        if (unlocked)
        {
            sceneLoader.GetComponent<SceneLoader>().LoadScene(stageId + 1); // 로딩 창 불러오기
        }
    }
}
