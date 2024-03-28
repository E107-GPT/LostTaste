using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // 카메라 위치 배열
    private Vector3[] positions = new Vector3[]
    {
        new Vector3(309.53f, 7.52f, -10.43f), // 첫 번째 스토리 위치
        new Vector3(309.53f, 7.52f, 16.5f), // 두 번째 스토리 위치
        new Vector3(309.53f, 7.52f, 41.07f)  // 세 번째 스토리 위치
    };

    private int currentPositionIndex = 0; // 현재 카메라 위치 인덱스

    private void Start()
    {
        // 시작 시 첫 번째 스토리 위치로 카메라 이동
        MoveCameraToPosition(0);
    }

    public void MoveToNextPosition()
    {
        // 다음 스토리 위치로 이동
        if (currentPositionIndex + 1 < positions.Length)
        {
            currentPositionIndex++;
            MoveCameraToPosition(currentPositionIndex);
        }
    }

    public void MoveToPreviousPosition()
    {
        // 이전 스토리 위치로 이동
        if (currentPositionIndex - 1 >= 0)
        {
            currentPositionIndex--;
            MoveCameraToPosition(currentPositionIndex);
        }
    }

    private void MoveCameraToPosition(int positionIndex)
    {
        // 지정된 인덱스의 위치로 카메라 이동
        transform.position = positions[positionIndex];
    }

    // 테스트용 기능: 다음, 이전 버튼에 연결
    public void NextButtonClicked()
    {
        MoveToNextPosition();
    }

    public void PrevButtonClicked()
    {
        MoveToPreviousPosition();
    }
}
