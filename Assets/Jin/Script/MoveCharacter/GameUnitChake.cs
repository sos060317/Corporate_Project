using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinCode.Utils;

public class GameUnitChake : MonoBehaviour
{
    //[SerializeField] private Transform selectionAreaTransform; // 선택 영역을 표시하는 Transform

    //private Vector3 startPosition; // 드래그 시작 지점
    //public List<Unit> selectedUnitRTSList; // 선택된 UnitRTS들을 담는 리스트

    //private void Awake()
    //{
    //    selectedUnitRTSList = new List<Unit>(); // 선택된 UnitRTS들을 담을 리스트 초기화
    //    selectionAreaTransform.gameObject.SetActive(false); // 선택 영역 비활성화
    //}

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // 왼쪽 마우스 버튼 클릭 시
    //        selectionAreaTransform.gameObject.SetActive(true); // 선택 영역 활성화
    //        startPosition = UtilsClass.GetMouseWorldPosition(); // 드래그 시작 지점을 현재 마우스 위치로 설정
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        // 왼쪽 마우스 버튼을 누른 상태로 드래그 중일 때
    //        Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
    //        Vector3 lowerLeft = new Vector3(
    //            Mathf.Min(startPosition.x, currentMousePosition.x),
    //            Mathf.Min(startPosition.y, currentMousePosition.y)
    //        );
    //        Vector3 upperRight = new Vector3(
    //            Mathf.Max(startPosition.x, currentMousePosition.x),
    //            Mathf.Max(startPosition.y, currentMousePosition.y)
    //        );
    //        selectionAreaTransform.position = lowerLeft; // 선택 영역의 위치를 시작 지점과 현재 마우스 위치의 최소값으로 설정
    //        selectionAreaTransform.localScale = upperRight - lowerLeft; // 선택 영역의 크기를 시작 지점과 현재 마우스 위치의 최대값과 최소값의 차이로 설정
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        // 왼쪽 마우스 버튼 누름 해제 시
    //        selectionAreaTransform.gameObject.SetActive(false); // 선택 영역 비활성화

    //        // 선택된 유닛들을 담을 배열
    //        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, UtilsClass.GetMouseWorldPosition());

    //        // 모든 UnitRTS를 선택 해제
    //        foreach (Unit unitRTS in selectedUnitRTSList)
    //        {
    //            unitRTS.SetSelectedVisible(false);
    //        }
    //        selectedUnitRTSList.Clear(); // 선택된 UnitRTS들을 담는 리스트 초기화

    //        // 선택 영역 안에 있는 유닛들을 선택
    //        foreach (Collider2D collider2D in collider2DArray)
    //        {
    //            Unit unitRTS = collider2D.GetComponent<Unit>();
    //            if (unitRTS != null)
    //            {
    //                unitRTS.SetSelectedVisible(true);
    //                selectedUnitRTSList.Add(unitRTS);
    //            }
    //        }

    //        Debug.Log(selectedUnitRTSList.Count); // 선택된 유닛들의 개수를 로그로 출력
    //    }

    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        // 오른쪽 마우스 버튼 클릭 시
    //        Vector3 moveToPosition = UtilsClass.GetMouseWorldPosition(); // 마우스 위치를 이동할 위치로 설정

    //        // 이동할 위치 주변의 위치 리스트를 가져옴
    //        List<Vector3> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 1f, 2f, 3f }, new int[] { 5, 10, 20 });

    //        int targetPositionListIndex = 0;

    //        // 선택된 유닛들을 주변 위치로 이동시킴
    //        foreach (Unit unitRTS in selectedUnitRTSList)
    //        {
    //            unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
    //            targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
    //        }
    //    }
    //}

    //// 시작 위치 주변의 위치 리스트를 가져옴
    //private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    //{
    //    List<Vector3> positionList = new List<Vector3>();
    //    positionList.Add(startPosition);
    //    for (int i = 0; i < ringDistanceArray.Length; i++)
    //    {
    //        positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
    //    }
    //    return positionList;
    //}

    //// 시작 위치 주변의 위치 리스트를 가져옴
    //private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    //{
    //    List<Vector3> positionList = new List<Vector3>();
    //    for (int i = 0; i < positionCount; i++)
    //    {
    //        float angle = i * (360f / positionCount);
    //        Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
    //        Vector3 position = startPosition + dir * distance;
    //        positionList.Add(position);
    //    }
    //    return positionList;
    //}

    //// 벡터를 주어진 각도로 회전시킴
    //private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    //{
    //    return Quaternion.Euler(0, 0, angle) * vec;
    //}

    [SerializeField] private Transform selectionAreaTransform; // 선택 영역을 표시하는 Transform

    private Vector3 startPosition; // 드래그 시작 지점
    public List<Unit> selectedUnitRTSList; // 선택된 UnitRTS들을 담는 리스트

    private void Awake()
    {
        selectedUnitRTSList = new List<Unit>(); // 선택된 UnitRTS들을 담을 리스트 초기화
        selectionAreaTransform.gameObject.SetActive(false); // 선택 영역 비활성화
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 왼쪽 마우스 버튼 클릭 시
            selectionAreaTransform.gameObject.SetActive(true); // 선택 영역 활성화
            startPosition = UtilsClass.GetMouseWorldPosition(); // 드래그 시작 지점을 현재 마우스 위치로 설정
        }

        if (Input.GetMouseButton(0))
        {
            // 왼쪽 마우스 버튼을 누른 상태로 드래그 중일 때
            Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y)
            );
            Vector3 upperRight = new Vector3(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y)
            );
            selectionAreaTransform.position = lowerLeft; // 선택 영역의 위치를 시작 지점과 현재 마우스 위치의 최소값으로 설정
            selectionAreaTransform.localScale = upperRight - lowerLeft; // 선택 영역의 크기를 시작 지점과 현재 마우스 위치의 최대값과 최소값의 차이로 설정
        }

        if (Input.GetMouseButtonUp(0))
        {
            // 왼쪽 마우스 버튼 누름 해제 시
            selectionAreaTransform.gameObject.SetActive(false); // 선택 영역 비활성화

            // 선택된 유닛들을 담을 배열
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, UtilsClass.GetMouseWorldPosition());

            // 모든 UnitRTS를 선택 해제
            foreach (Unit unitRTS in selectedUnitRTSList)
            {
                unitRTS.SetSelectedVisible(false);
            }
            selectedUnitRTSList.Clear(); // 선택된 UnitRTS들을 담는 리스트 초기화

            // 선택 영역 안에 있는 유닛들을 선택
            foreach (Collider2D collider2D in collider2DArray)
            {
                Unit unitRTS = collider2D.GetComponent<Unit>();
                if (unitRTS != null)
                {
                    unitRTS.SetSelectedVisible(true);
                    selectedUnitRTSList.Add(unitRTS);
                }
            }

            Debug.Log(selectedUnitRTSList.Count); // 선택된 유닛들의 개수를 로그로 출력
        }

        if (Input.GetMouseButtonDown(1))
        {
            // 오른쪽 마우스 버튼 클릭 시
            Vector3 moveToPosition = UtilsClass.GetMouseWorldPosition(); // 마우스 위치를 이동할 위치로 설정

            // 이동할 위치 주변의 위치 리스트를 가져옴
            List<Vector3> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 1f, 2f, 3f }, new int[] { 5, 10, 20 });

            int targetPositionListIndex = 0;

            // 선택된 유닛들을 주변 위치로 이동시킴
            foreach (Unit unitRTS in selectedUnitRTSList)
            {
                unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
                targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
            }
        }
    }

    // 시작 위치 주변의 위치 리스트를 가져옴
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    {
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPosition);
        for (int i = 0; i < ringDistanceArray.Length; i++)
        {
            positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }

    // 시작 위치 주변의 위치 리스트를 가져옴
    private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }

    // 벡터를 주어진 각도로 회전시킴
    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }
}
