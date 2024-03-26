using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI; // NavMeshAgent 사용을 위해 필요
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MovePlayerToCamp : MonoBehaviour
{
    // 캠프 위치
    [Header("[ 캠프 위치 ]")]
    public Transform campLocation; // 캠프 위치를 저장하는 변수

    // 확인 버튼
    [Header("[ 확인 버튼 ]")]
    public Button confirmButton;

    // 스크립트가 활성화되었을 때 호출되는 메서드
    private void Awake()
    {
        // 버튼에 클릭 이벤트를 추가
        if (confirmButton != null)
            this.confirmButton.onClick.AddListener(LoadCampScene);
    }

    // Camp Scene을 로드하는 메서드
    public void LoadCampScene()
    {
        // "Dungeon" 씬을 LoadSceneMode.Single 모드로 로드합니다.
        //SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
        Managers.Scene.LoadScene(Define.Scene.Dungeon, true);
        
    }

    // '확인' 버튼 클릭 시 호출될 메서드
    //public void OnConfirmButtonClicked()
    //{
    //    GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 태그를 사용하여 플레이어 객체 찾기
    //    if (player != null)
    //    {
    //        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
    //        if (agent != null)
    //        {
    //            // NavMeshAgent가 있는 경우, Warp 메서드를 사용하여 캠프 위치로 이동
    //            agent.Warp(campLocation.position);
    //        }
    //        else
    //        {
    //            // NavMeshAgent가 없는 경우, 직접 위치를 설정
    //            player.transform.position = campLocation.position;
    //        }
    //    }
    //}
}


