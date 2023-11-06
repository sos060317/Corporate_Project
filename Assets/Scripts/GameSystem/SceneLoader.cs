using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loaderUI;    // 로딩 UI 창
    [SerializeField] private Slider progressSlider;  // 로딩 바

    [HideInInspector] public bool isCredit = false;

    private bool isExit = false;

    public void LoadScene(int index)
    {
        StartCoroutine(LoadScene_Coroutine(index)); // 코루틴 호줄
    }

    public IEnumerator LoadScene_Coroutine(int index)
    {
        progressSlider.value = 0; // 로딩 바 초기화
        loaderUI.SetActive(true); // 로딩 UI 창 보여주기   
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index); // 비동기로 씬을 불러옴
        asyncOperation.allowSceneActivation = false; // 씬이 로드되더라도 장면이 전환되지 않도록 함
        float progress = 0; // 로딩 정도를 나타내는 변수 생성
        while (!asyncOperation.isDone) // 로딩이 끝나지 않았다면
        {
            // 정해진 속도로 현재 씬 로딩 정도를 계산한다
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            progressSlider.value = progress; // 계산한 그 값을 적용한다
            if (progress >= 0.9f) // 만약 최대 값에 도달하면
            {
                progressSlider.value = 1; // 로딩 값을 1로 바꿔 로딩창을 꽉 채우게 보여줌
                asyncOperation.allowSceneActivation = true; // 그리고 씬을 로딩함
            }
            yield return null; // 한 프레임 대기
        }
    }
}