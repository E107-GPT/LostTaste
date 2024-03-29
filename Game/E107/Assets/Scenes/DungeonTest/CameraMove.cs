using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // 카메라 위치 배열
    private Vector3[] positions = new Vector3[]
    {
        new Vector3(7.0f, 2.0f, 50.0f), // 첫 번째 스토리 위치
        new Vector3(7.0f, 2.0f, 100.0f), 
        new Vector3(7.0f, 2.0f, 150.0f),
        new Vector3(7.0f, 2.0f, 200.0f),
        new Vector3(7.0f, 2.0f, 250.0f),
    };

    private int currentPositionIndex = 0; // 현재 카메라 위치 인덱스

    private void Start()
    {
        StartCoroutine(MoveCameraPeriodically());
    }

    IEnumerator MoveCameraPeriodically()
    {
        while (true)
        {
            MoveCameraToPosition(currentPositionIndex);
            // 현재 위치에서 다음 위치로 인덱스 업데이트
            currentPositionIndex = (currentPositionIndex + 1) % positions.Length;
            // 카메라 위치 이동
            MoveCameraToPosition(currentPositionIndex);
            // 3초 대기
            yield return new WaitForSeconds(3f);
        }
    }

    private void MoveCameraToPosition(int positionIndex)
    {
        // 지정된 인덱스의 위치로 카메라 이동
        transform.position = positions[positionIndex];
    }

}
