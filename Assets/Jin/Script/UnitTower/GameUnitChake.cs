using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEditor;
using JinCode.Utils;

public class GameUnitChake : MonoBehaviour
{
    public UnitWindow UnitUI;

    public GameObject Tower;
    public GameObject Range;
    public List<Unit> selectedUnitRTSList;

    public bool clicked = false;
    private bool UToggle = true;

    public Vector3 lastRightClickPosition; // 가장 최근 우클릭 위치 저장

    private void Awake()
    {
        selectedUnitRTSList = new List<Unit>();
        Range.SetActive(false); // 초기에 Range를 비활성화
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (gameObject.CompareTag("SpawnTower"))
        {
            clicked = true;
            SelectUnitsInTower();
            Range.SetActive(true); // Tower 클릭 시 Range 활성화
            UToggle = false;
        }
    }

    private void Update()
    {
        if(clicked == true)
        {
            //if (Input.GetMouseButtonDown(1))
            //{
            //    RaycastHit2D hit = Physics2D.Raycast(UtilsClass.GetMouseWorldPosition(), Vector2.up);

            //    if (hit.collider != null && hit.collider.gameObject == Range && hit.collider.CompareTag("Road"))
            //    {
            //        Debug.Log("Hit Object Tag: " + hit.collider.tag);
            //        lastRightClickPosition = UtilsClass.GetMouseWorldPosition(); // 우클릭한 위치 저장

            //        List<Vector3> targetPositionList = GetPositionListAround(lastRightClickPosition, new float[] { 0.8f, 0.9f, 1f }, new int[] { 5, 10, 20 });
            //        int targetPositionListIndex = 0;

            //        foreach (Unit unitRTS in selectedUnitRTSList)
            //        {
            //            unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
            //            targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
            //        }

            //        selectedUnitRTSList.Clear();
            //        clicked = false;

            //        foreach (Unit unitRTS in FindObjectsOfType<Unit>())
            //        {
            //            unitRTS.SetSelectedVisible(false);
            //        }

            //        Range.SetActive(false); // 우클릭 시 Range 비활성화

            //        Debug.Log("우클릭 위치값 : " + lastRightClickPosition);
            //    }
            //    else
            //    {
            //        selectedUnitRTSList.Clear();
            //        Range.SetActive(false);
            //        Debug.Log("범위 밖");
            //        clicked = false;
            //    }
            //}

            StartCoroutine(DelayTm());

            if(UToggle == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        clicked = false;
                        Range.SetActive(false);
                        UToggle = false;
                    }
                }
            }
            

            if (UnitUI.buttonDown == true)
            {
                clicked = false;
                Range.SetActive(false);
                UnitUI.buttonDown = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Move();
            }

        }


    }

    private IEnumerator DelayTm()
    {
        yield return new WaitForSeconds(0.2f);
        UToggle = true;
    }

    private void Move()
    {
        bool isR = false;
        bool isLod = false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(UtilsClass.GetMouseWorldPosition(), Vector2.zero);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Road")) isLod = true;
            if (hit.collider.gameObject == Range) isR = true;
        }
        if(isLod && isR)
        {
            lastRightClickPosition = UtilsClass.GetMouseWorldPosition(); // 우클릭한 위치 저장

            List<Vector3> targetPositionList = GetPositionListAround(lastRightClickPosition, new float[] { 0.8f, 0.9f, 1f }, new int[] { 5, 10, 20 });
            int targetPositionListIndex = 0;

            foreach (Unit unitRTS in selectedUnitRTSList)
            {
                unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
                targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
            }

            selectedUnitRTSList.Clear();
            clicked = false;

            foreach (Unit unitRTS in FindObjectsOfType<Unit>())
            {
                unitRTS.SetSelectedVisible(false);
            }

            Range.SetActive(false); // 우클릭 시 Range 비활성화

            //Debug.Log("우클릭 위치값 : " + lastRightClickPosition);
        }
        else
        {
            selectedUnitRTSList.Clear();
            Range.SetActive(false);
            //Debug.Log("범위 밖");
            clicked = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(lastRightClickPosition, 0.1f); // 우클릭 위치를 시각화
    }


    private void SelectUnitsInTower()
    {
        foreach (Transform child in Tower.transform)
        {
            Unit unitRTS = child.GetComponent<Unit>();
            if (unitRTS != null && unitRTS.gameObject.activeSelf)
            {
                unitRTS.SetSelectedVisible(true);
                selectedUnitRTSList.Add(unitRTS);
            }
        }
    }

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

    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }
}