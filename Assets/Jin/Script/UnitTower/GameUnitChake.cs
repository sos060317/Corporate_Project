using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JinCode.Utils;

public class GameUnitChake : MonoBehaviour
{
    //public GameObject Tower;
    //public GameObject Range;
    //public List<Unit> selectedUnitRTSList;

    //[SerializeField] private bool clicked = false;

    //private void Awake()
    //{
    //    selectedUnitRTSList = new List<Unit>();
    //    Range.SetActive(false); // �ʱ⿡ Range�� ��Ȱ��ȭ
    //}

    //private void OnMouseDown()
    //{
    //    if (gameObject.CompareTag("SpawnTower"))
    //    {
    //        clicked = true;
    //        SelectUnitsInTower();
    //        Range.SetActive(true); // Tower Ŭ�� �� Range Ȱ��ȭ
    //    }
    //}

    //private void Update()
    //{
    //    selectedUnitRTSList.RemoveAll(item => item == null);
    //    for (int i = selectedUnitRTSList.Count - 1; i >= 0; i--)
    //    {
    //        if (EditorUtility.IsPersistent(selectedUnitRTSList[i]))
    //        {
    //            selectedUnitRTSList.RemoveAt(i);
    //        }
    //    }

    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        RaycastHit2D hit = Physics2D.Raycast(UtilsClass.GetMouseWorldPosition(), Vector2.zero);
    //        if (hit.collider != null && hit.collider.gameObject == Range)
    //        {
    //            Vector3 moveToPosition = UtilsClass.GetMouseWorldPosition();
    //            List<Vector3> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 1f, 2f, 3f }, new int[] { 5, 10, 20 });
    //            int targetPositionListIndex = 0;

    //            foreach (Unit unitRTS in selectedUnitRTSList)
    //            {
    //                unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
    //                targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
    //            }

    //            selectedUnitRTSList.Clear();
    //            clicked = false;

    //            foreach (Unit unitRTS in FindObjectsOfType<Unit>())
    //            {
    //                unitRTS.SetSelectedVisible(false);
    //            }

    //            Range.SetActive(false); // ��Ŭ�� �� Range ��Ȱ��ȭ
    //        }
    //    }
    //}

    //private void SelectUnitsInTower()
    //{
    //    foreach (Transform child in Tower.transform)
    //    {
    //        Unit unitRTS = child.GetComponent<Unit>();
    //        if (unitRTS != null)
    //        {
    //            unitRTS.SetSelectedVisible(true);
    //            selectedUnitRTSList.Add(unitRTS);
    //        }
    //    }
    //}

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

    //private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    //{
    //    return Quaternion.Euler(0, 0, angle) * vec;
    //}


    // ���̺��  |  ��

    public GameObject Tower;
    public GameObject Range;
    public List<Unit> selectedUnitRTSList;

    public bool clicked = false;

    public Vector3 lastRightClickPosition; // ���� �ֱ� ��Ŭ�� ��ġ ����

    private void Awake()
    {
        selectedUnitRTSList = new List<Unit>();
        Range.SetActive(false); // �ʱ⿡ Range�� ��Ȱ��ȭ
    }

    private void OnMouseDown()
    {

        if (gameObject.CompareTag("SpawnTower"))
        {
            clicked = true;
            SelectUnitsInTower();

            Range.SetActive(true); // Tower Ŭ�� �� Range Ȱ��ȭ
        }
    }

    private void Update()
    {

        selectedUnitRTSList.RemoveAll(item => item == null);
        for (int i = selectedUnitRTSList.Count - 1; i >= 0; i--)
        {
            if (EditorUtility.IsPersistent(selectedUnitRTSList[i]))
            {
                selectedUnitRTSList.RemoveAt(i);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(UtilsClass.GetMouseWorldPosition(), Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == Range)
            {
                lastRightClickPosition = UtilsClass.GetMouseWorldPosition(); // ��Ŭ���� ��ġ ����

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

                Range.SetActive(false); // ��Ŭ�� �� Range ��Ȱ��ȭ

                Debug.Log("��Ŭ�� ��ġ�� : " + lastRightClickPosition);
            }
            else
            {
                selectedUnitRTSList.Clear();
                Range.SetActive(false);
                Debug.Log("���� ��");
                clicked = false;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(lastRightClickPosition, 0.1f); // ��Ŭ�� ��ġ�� �ð�ȭ
    }


    private void SelectUnitsInTower()
    {
        foreach (Transform child in Tower.transform)
        {
            Unit unitRTS = child.GetComponent<Unit>();
            if (unitRTS != null)
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