using UnityEngine;

// 카메라 줌 인 아웃
public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed; // 줌 인 아웃 속도
    [SerializeField] private float maxZoomIn; // 줌 인 아웃 속도
    [SerializeField] private float maxZoomOut; // 줌 인 아웃 속도

    private float tempValue; // 최대 최소 값을 벗어나지 못하게 해주는 변수

    private Camera camera; // 카메라

    private void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>(); // 카메라 할당
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; // 마우스 휠 값 * 줌 인 아웃 속도

        if (camera.orthographicSize <= maxZoomIn && scroll > 0) // 최대 줌일 때, 확대하는 것 방지 
        {
            tempValue = camera.orthographicSize; // 현재 자신의 크기를 tempValue에 담아두고
            camera.orthographicSize = tempValue; // 계속해서 크기를 고정
        }
        else if (camera.orthographicSize >= maxZoomOut && scroll < 0) // 최소 줌 일 때, 축소하는 것 방지
        {
            tempValue = camera.orthographicSize; // 현재 자신의 크기를 tempValue에 담아두고
            camera.orthographicSize = tempValue; // 계속해서 크기를 고정
        }
        else
        {
            camera.orthographicSize -= scroll * 0.5f; // scroll 값만큼 카메라 크기에 적용
        }
    }
}
