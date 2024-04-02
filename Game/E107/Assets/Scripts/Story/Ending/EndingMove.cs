using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingMove : MonoBehaviour
{
    // 카메라 위치 배열
    private Vector3[] positions = new Vector3[]
    {
        new Vector3(2.92f, 1.04f, 5.52f), // 용사의 마지막 공격

        
    };

    // 카메라 회전 배열
    private Quaternion[] rotations = new Quaternion[]
    {
        Quaternion.Euler(-8.429f, -121.765f, 3.607f),
        
    };

    private int currentPositionIndex = 0; // 현재 카메라 위치 인덱스

    public EndingSceneManager EndingSceneManager;

    public TextMeshProUGUI storyText;

    private string[] storyContents = new string[]
    {
        "끝이다, 마왕!!!",
        
    };

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
            yield return new WaitForSeconds(6f);

            // 현재 위치에서 다음 위치로 인덱스 업데이트
            currentPositionIndex++;

            // 카메라 위치 이동
            if (currentPositionIndex < positions.Length)
            {
                MoveCameraToPosition(currentPositionIndex);
            }
            if (currentPositionIndex == positions.Length - 1)
            {
                yield return new WaitForSeconds(6f);
                EndingSceneManager.CompleteStory(); // 스토리 완료 처리
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
            string processedText = storyContents[positionIndex].Replace("/", "\n");
            storyText.text = processedText;
        }
    }

}
