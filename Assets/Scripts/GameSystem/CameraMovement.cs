using System;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam; // 카메라 할당

    [SerializeField] private float zoomSpeed; // 줌 인 아웃 속도
    [SerializeField] private float maxZoomIn; // 최대 줌 인
    [SerializeField] private float maxZoomOut; // 최대 줌 아웃

    [SerializeField] private SpriteRenderer mapRenderer;

    [SerializeField] private GameObject indicatorColObj;

    private float tempValue; // 최대 최소 값을 벗어나지 못하게 해주는 변수
    private float mapMinX, mapMaxX, mapMinY, mapMaxY; // 맵의 가로 세로 최소 최대 크기
    private float baseSize;

    private Vector3 dragOrigin; // 마우스가 처음 클릭 됐을 때의 벡터 받아오기
    private Vector3 tempOrigin;

    private void Awake()
    {
        // 해당 스프라이트의 x, y값을 구하여 각각 절반을 더하고 빼서 그 스프라이트의 끝 값을 알아냄
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;

        baseSize = cam.orthographicSize;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameStop)
        {
            return;
        }

        PanCamera();
        ScrollCamera();
    }

    private void PanCamera()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) // 마우스를 처음 클릭했을 때
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition); // 그 벡터를 받아옴x
        }

        if (Input.GetMouseButton(0)) // 마우스를 움직일 때
        {
            if(dragOrigin == tempOrigin)
            {
                dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition); // 움직이는 벡터를 저장함

            // 움직인 벡터를 저장한 만큼 카메라를 이동함 and 카메라 제한
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }

        if(Input.GetMouseButtonUp(0))
        {
            tempOrigin = dragOrigin;
        }
    }

    private void ScrollCamera()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed; // 마우스 휠 값 * 줌 인 아웃 속도

        if (cam.orthographicSize <= maxZoomIn && scroll > 0) // 최대 줌일 때, 확대하는 것 방지 
        {
            // 크기를 고정
            cam.orthographicSize = maxZoomIn;
        }
        else if (cam.orthographicSize >= maxZoomOut && scroll < 0) // 최소 줌 일 때, 축소하는 것 방지
        {
            // 크기를 고정
            cam.orthographicSize = maxZoomOut;
        }
        else
        {
            cam.orthographicSize -= scroll * 0.5f; // scroll 값만큼 카메라 크기에 적용
            
            // 인디케이터 콜라이더의 크기를 고정하기 위한 수식
            indicatorColObj.transform.localScale = new Vector3(cam.orthographicSize / baseSize,
                cam.orthographicSize / baseSize, cam.orthographicSize / baseSize);
        }
        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;             // 카메라 높이 구하기
        float camWidth = cam.orthographicSize * cam.aspect; // 카메라 너비 구하기

        float minX = mapMinX + camWidth;  // 왼쪽 카메라 제한
        float maxX = mapMaxX - camWidth;  // 오른쪽 카메라 제한
        float minY = mapMinY + camHeight; // 위쪽 카메라 제한
        float maxY = mapMaxY - camHeight; // 아래쪽 카메라 제한

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX); // 좌우 카메라 제한
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY); // 위아래 카메라 제한

        return new Vector3(newX, newY, targetPosition.z); // 포지션 적용
    }
}