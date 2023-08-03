using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinCode.Utils;

public class GameUnitChake : MonoBehaviour
{
    //[SerializeField] private Transform selectionAreaTransform; // ���� ������ ǥ���ϴ� Transform

    //private Vector3 startPosition; // �巡�� ���� ����
    //public List<Unit> selectedUnitRTSList; // ���õ� UnitRTS���� ��� ����Ʈ

    //private void Awake()
    //{
    //    selectedUnitRTSList = new List<Unit>(); // ���õ� UnitRTS���� ���� ����Ʈ �ʱ�ȭ
    //    selectionAreaTransform.gameObject.SetActive(false); // ���� ���� ��Ȱ��ȭ
    //}

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // ���� ���콺 ��ư Ŭ�� ��
    //        selectionAreaTransform.gameObject.SetActive(true); // ���� ���� Ȱ��ȭ
    //        startPosition = UtilsClass.GetMouseWorldPosition(); // �巡�� ���� ������ ���� ���콺 ��ġ�� ����
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        // ���� ���콺 ��ư�� ���� ���·� �巡�� ���� ��
    //        Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
    //        Vector3 lowerLeft = new Vector3(
    //            Mathf.Min(startPosition.x, currentMousePosition.x),
    //            Mathf.Min(startPosition.y, currentMousePosition.y)
    //        );
    //        Vector3 upperRight = new Vector3(
    //            Mathf.Max(startPosition.x, currentMousePosition.x),
    //            Mathf.Max(startPosition.y, currentMousePosition.y)
    //        );
    //        selectionAreaTransform.position = lowerLeft; // ���� ������ ��ġ�� ���� ������ ���� ���콺 ��ġ�� �ּҰ����� ����
    //        selectionAreaTransform.localScale = upperRight - lowerLeft; // ���� ������ ũ�⸦ ���� ������ ���� ���콺 ��ġ�� �ִ밪�� �ּҰ��� ���̷� ����
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        // ���� ���콺 ��ư ���� ���� ��
    //        selectionAreaTransform.gameObject.SetActive(false); // ���� ���� ��Ȱ��ȭ

    //        // ���õ� ���ֵ��� ���� �迭
    //        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, UtilsClass.GetMouseWorldPosition());

    //        // ��� UnitRTS�� ���� ����
    //        foreach (Unit unitRTS in selectedUnitRTSList)
    //        {
    //            unitRTS.SetSelectedVisible(false);
    //        }
    //        selectedUnitRTSList.Clear(); // ���õ� UnitRTS���� ��� ����Ʈ �ʱ�ȭ

    //        // ���� ���� �ȿ� �ִ� ���ֵ��� ����
    //        foreach (Collider2D collider2D in collider2DArray)
    //        {
    //            Unit unitRTS = collider2D.GetComponent<Unit>();
    //            if (unitRTS != null)
    //            {
    //                unitRTS.SetSelectedVisible(true);
    //                selectedUnitRTSList.Add(unitRTS);
    //            }
    //        }

    //        Debug.Log(selectedUnitRTSList.Count); // ���õ� ���ֵ��� ������ �α׷� ���
    //    }

    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        // ������ ���콺 ��ư Ŭ�� ��
    //        Vector3 moveToPosition = UtilsClass.GetMouseWorldPosition(); // ���콺 ��ġ�� �̵��� ��ġ�� ����

    //        // �̵��� ��ġ �ֺ��� ��ġ ����Ʈ�� ������
    //        List<Vector3> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 1f, 2f, 3f }, new int[] { 5, 10, 20 });

    //        int targetPositionListIndex = 0;

    //        // ���õ� ���ֵ��� �ֺ� ��ġ�� �̵���Ŵ
    //        foreach (Unit unitRTS in selectedUnitRTSList)
    //        {
    //            unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
    //            targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
    //        }
    //    }
    //}

    //// ���� ��ġ �ֺ��� ��ġ ����Ʈ�� ������
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

    //// ���� ��ġ �ֺ��� ��ġ ����Ʈ�� ������
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

    //// ���͸� �־��� ������ ȸ����Ŵ
    //private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    //{
    //    return Quaternion.Euler(0, 0, angle) * vec;
    //}

    [SerializeField] private Transform selectionAreaTransform; // ���� ������ ǥ���ϴ� Transform

    private Vector3 startPosition; // �巡�� ���� ����
    public List<Unit> selectedUnitRTSList; // ���õ� UnitRTS���� ��� ����Ʈ

    private void Awake()
    {
        selectedUnitRTSList = new List<Unit>(); // ���õ� UnitRTS���� ���� ����Ʈ �ʱ�ȭ
        selectionAreaTransform.gameObject.SetActive(false); // ���� ���� ��Ȱ��ȭ
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���� ���콺 ��ư Ŭ�� ��
            selectionAreaTransform.gameObject.SetActive(true); // ���� ���� Ȱ��ȭ
            startPosition = UtilsClass.GetMouseWorldPosition(); // �巡�� ���� ������ ���� ���콺 ��ġ�� ����
        }

        if (Input.GetMouseButton(0))
        {
            // ���� ���콺 ��ư�� ���� ���·� �巡�� ���� ��
            Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y)
            );
            Vector3 upperRight = new Vector3(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y)
            );
            selectionAreaTransform.position = lowerLeft; // ���� ������ ��ġ�� ���� ������ ���� ���콺 ��ġ�� �ּҰ����� ����
            selectionAreaTransform.localScale = upperRight - lowerLeft; // ���� ������ ũ�⸦ ���� ������ ���� ���콺 ��ġ�� �ִ밪�� �ּҰ��� ���̷� ����
        }

        if (Input.GetMouseButtonUp(0))
        {
            // ���� ���콺 ��ư ���� ���� ��
            selectionAreaTransform.gameObject.SetActive(false); // ���� ���� ��Ȱ��ȭ

            // ���õ� ���ֵ��� ���� �迭
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, UtilsClass.GetMouseWorldPosition());

            // ��� UnitRTS�� ���� ����
            foreach (Unit unitRTS in selectedUnitRTSList)
            {
                unitRTS.SetSelectedVisible(false);
            }
            selectedUnitRTSList.Clear(); // ���õ� UnitRTS���� ��� ����Ʈ �ʱ�ȭ

            // ���� ���� �ȿ� �ִ� ���ֵ��� ����
            foreach (Collider2D collider2D in collider2DArray)
            {
                Unit unitRTS = collider2D.GetComponent<Unit>();
                if (unitRTS != null)
                {
                    unitRTS.SetSelectedVisible(true);
                    selectedUnitRTSList.Add(unitRTS);
                }
            }

            Debug.Log(selectedUnitRTSList.Count); // ���õ� ���ֵ��� ������ �α׷� ���
        }

        if (Input.GetMouseButtonDown(1))
        {
            // ������ ���콺 ��ư Ŭ�� ��
            Vector3 moveToPosition = UtilsClass.GetMouseWorldPosition(); // ���콺 ��ġ�� �̵��� ��ġ�� ����

            // �̵��� ��ġ �ֺ��� ��ġ ����Ʈ�� ������
            List<Vector3> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 1f, 2f, 3f }, new int[] { 5, 10, 20 });

            int targetPositionListIndex = 0;

            // ���õ� ���ֵ��� �ֺ� ��ġ�� �̵���Ŵ
            foreach (Unit unitRTS in selectedUnitRTSList)
            {
                unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
                targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
            }
        }
    }

    // ���� ��ġ �ֺ��� ��ġ ����Ʈ�� ������
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

    // ���� ��ġ �ֺ��� ��ġ ����Ʈ�� ������
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

    // ���͸� �־��� ������ ȸ����Ŵ
    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }
}
