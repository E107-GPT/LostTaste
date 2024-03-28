using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // ī�޶� ��ġ �迭
    private Vector3[] positions = new Vector3[]
    {
        new Vector3(309.53f, 7.52f, -10.43f), // ù ��° ���丮 ��ġ
        new Vector3(309.53f, 7.52f, 16.5f), // �� ��° ���丮 ��ġ
        new Vector3(309.53f, 7.52f, 41.07f)  // �� ��° ���丮 ��ġ
    };

    private int currentPositionIndex = 0; // ���� ī�޶� ��ġ �ε���

    private void Start()
    {
        // ���� �� ù ��° ���丮 ��ġ�� ī�޶� �̵�
        MoveCameraToPosition(0);
    }

    public void MoveToNextPosition()
    {
        // ���� ���丮 ��ġ�� �̵�
        if (currentPositionIndex + 1 < positions.Length)
        {
            currentPositionIndex++;
            MoveCameraToPosition(currentPositionIndex);
        }
    }

    public void MoveToPreviousPosition()
    {
        // ���� ���丮 ��ġ�� �̵�
        if (currentPositionIndex - 1 >= 0)
        {
            currentPositionIndex--;
            MoveCameraToPosition(currentPositionIndex);
        }
    }

    private void MoveCameraToPosition(int positionIndex)
    {
        // ������ �ε����� ��ġ�� ī�޶� �̵�
        transform.position = positions[positionIndex];
    }

    // �׽�Ʈ�� ���: ����, ���� ��ư�� ����
    public void NextButtonClicked()
    {
        MoveToNextPosition();
    }

    public void PrevButtonClicked()
    {
        MoveToPreviousPosition();
    }
}
