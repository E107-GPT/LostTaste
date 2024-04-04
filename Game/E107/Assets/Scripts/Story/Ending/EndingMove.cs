using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingMove : MonoBehaviour
{
    // 카메라 위치 배열
    private Vector3[] positions = new Vector3[]
    {
        new Vector3(271.5f, 21.2f, -479.4f), // 1.쓰러진 용사들
        new Vector3(166.74f, 14.08f, -312.35f), // 2.쓰러진 용사들과 용사와 마왕의 대치
        new Vector3(30f, 6.2f, -169.3f), // 3.용사와 마왕의 대치
        new Vector3(2.92f, 1.04f, 5.52f), // 4.용사의 마지막 공격
        new Vector3(196f, 28f, 332.1f), // 5. 마왕의 저주가 풀리는 장면
        new Vector3(382.67f, 85.16f, 651.37f), // 6.마을에서 환영받는 용사들
        //new Vector3(498.78f, 77.1f, 835.79f), // 7.뒷풀이 하는 용사들
    };

    // 카메라 회전 배열
    private Quaternion[] rotations = new Quaternion[]
    {
        Quaternion.Euler(90f, -117.983f, 0f),   // 1.쓰러진 용사들
        Quaternion.Euler(26.461f, -117.983f, 0f),   // 2.쓰러진 용사들과 용사와 마왕의 대치
        Quaternion.Euler(0f, -120.669f, 0f),    // 3.용사와 마왕의 대치
        Quaternion.Euler(-8.429f, -121.765f, 3.607f),   // 4.용사의 마지막 공격
        Quaternion.Euler(-31.128f, -61.675f, 0f),   // 5. 마왕의 저주가 풀리는 장면
        Quaternion.Euler(17.48f, -33.526f, 1.233f),   // 6.마을에서 환영받는 용사들
        //Quaternion.Euler(22.767f, 17.795f, 1.336f),   // 7.뒷풀이 하는 용사들
    };

    // 장면에서 대기할 시간
    private float[] waitTimes = new float[]
    {
        5f, // 1.쓰러진 용사들
        5f, // 2.쓰러진 용사들과 용사와 마왕의 대치
        5f, // 3.용사와 마왕의 대치
        3f, // 4.용사의 마지막 공격
        6f, // 5. 마왕의 저주가 풀리는 장면
        8f, // 6.마을에서 환영받는 용사들
        //3f, // 7.뒷풀이 하는 용사들
    };

    private string[] storyContents = new string[]
    {
        "", // 1.쓰러진 용사들
        "끝을 내자, 용사여.",  // 2.쓰러진 용사들과 용사와 마왕의 대치
        "우리가 세계에 맛을 되찾겠다! ",    // 3.용사와 마왕의 대치
        "여기서 끝이다, 마왕!!!",   // 4.용사의 마지막 공격
        "", // 5. 마왕의 저주가 풀리는 장면
        "용사들의 여정 끝에 맛의 기쁨이 되살아났고 / 세상은 다시 삶의 풍미를 느낄 수 있게 되었습니다.",    // 6.마을에서 환영받는 용사들
        //"용사들의 용기 덕분에 / 세상은 다시금 평화를 되찾았습니다." //7.뒷풀이 하는 용사들
    };
    private int currentPositionIndex = 0; // 현재 카메라 위치 인덱스

    public EndingSceneManager EndingSceneManager;

    public TextMeshProUGUI storyText;

    public GameObject[] effects;

    public AudioClip[] backgroundMusics; 
    private AudioSource audioSource; 


    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource 컴포넌트 가져오기
        if (audioSource == null)
        {
            // AudioSource 컴포넌트가 없다면 추가
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartCoroutine(MoveCameraPeriodically());
    }

    IEnumerator MoveCameraPeriodically()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (i >= 1)
            {
                effects[i - 1].SetActive(false);
            }

            MoveCameraToPosition(i);

            if (i < effects.Length)
            {
                effects[i].SetActive(true); // 해당 장면의 이펙트 활성화
            }

            if (i < backgroundMusics.Length && backgroundMusics[i] != null)
            {
                audioSource.clip = backgroundMusics[i];
                audioSource.Play();
            }

            yield return new WaitForSeconds(waitTimes[i]);
        }
        EndingSceneManager.CompleteStory(); // 모든 스토리 장면이 끝나면 호출
    }

    private void MoveCameraToPosition(int positionIndex)
    {
        // 지정된 인덱스의 위치로 카메라 이동
        transform.position = positions[positionIndex];
        transform.rotation = rotations[positionIndex];
        string processedText = storyContents[positionIndex].Replace("/", "\n");
        storyText.text = processedText;
    }

}
