using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // 카메라 위치 배열
    private Vector3[] positions = new Vector3[]
    {
        new Vector3(9.444f, 5.65f, 49.65f), // 평화로운 마을
        new Vector3(4.49f, 2.29f, 99.94f),  // 마왕의 저주
        new Vector3(5.22f, 3.293f, 149.316f),   // 중간 보스들
        new Vector3(3.45f, 1.39f, 201.08f),    // 용사들의 결집
        new Vector3(5.96f, 1.1f, 247.63f),    // 모험의 시작
    };

    // 카메라 회전 배열
    private Quaternion[] rotations = new Quaternion[]
    {
        Quaternion.Euler(44.261f, -90f, 0),
        Quaternion.Euler(-16.847f, -90f, 0),
        Quaternion.Euler(12.621f, -79.192f, -1.285f),
        Quaternion.Euler(0, -90f, 0),
        Quaternion.Euler(-15.902f, -71.791f, 0),
    };

    private int currentPositionIndex = 0; // 현재 카메라 위치 인덱스

    public LoadingSceneManager loadingSceneManager;

    private void Start()
    {
        MoveCameraToPosition(currentPositionIndex);

        StartCoroutine(MoveCameraPeriodically());
    }

    IEnumerator MoveCameraPeriodically()
    {
        while (currentPositionIndex < positions.Length - 1)
        {
            // 일정 시간 대기
            yield return new WaitForSeconds(3f);

            // 현재 위치에서 다음 위치로 인덱스 업데이트
            currentPositionIndex++;

            // 카메라 위치 이동
            if (currentPositionIndex < positions.Length)
            {
                MoveCameraToPosition(currentPositionIndex);
            }
            if (currentPositionIndex == positions.Length - 1)
            {
                yield return new WaitForSeconds(3f);
                loadingSceneManager.CompleteStory(); // 스토리 완료 처리
            }
        }
    }

    private void MoveCameraToPosition(int positionIndex)
    {
        // 지정된 인덱스의 위치로 카메라 이동
        if (positionIndex >= 0 && positionIndex < positions.Length)
        {
            transform.position = positions[positionIndex];
            transform.rotation = rotations[positionIndex];
        }
    }

}
