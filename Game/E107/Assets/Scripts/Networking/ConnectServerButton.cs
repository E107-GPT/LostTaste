using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ConnectServerButton : MonoBehaviour
{
    public GameObject[] serverButton;
    string[] appId;
    void Start()
    {
        appId = new string[3];
        appId[0] = "3cace9da-35aa-49cd-a454-f748a53ca1ef";
        appId[1] = "91851ade-708c-4a66-8f90-5b526eba80a2";
        appId[2] = "169642ab-8e8d-42f1-bb00-ffeffe4d038c";
        StartCoroutine(ServerColor());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator ServerColor()
    {
        // serverButton 배열의 각 요소에 대해 반복
        for (int i = 0; i < serverButton.Length; i++)
        {
            // 현재 서버의 AppId를 설정
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime = appId[i];

            // Photon 서버에 연결 시도
            PhotonNetwork.ConnectUsingSettings();

            // 연결될 때까지 대기
            while (!PhotonNetwork.IsConnected)
            {
                yield return null;
            }

            // 연결 후, 현재 플레이어 수를 로그로 출력
            Debug.Log(PhotonNetwork.IsConnected +" / " +  (i + 1) + " : " + PhotonNetwork.CountOfPlayers);

            // 플레이어 수에 따라 서버 버튼의 색상 변경
            if (PhotonNetwork.CountOfPlayers > 19)
                serverButton[i].GetComponent<Image>().color = Color.yellow;
            else if (PhotonNetwork.CountOfPlayers > 10)
                serverButton[i].GetComponent<Image>().color = Color.red;

            // 현재 서버 연결 해제
            PhotonNetwork.Disconnect();

            // 연결 해제될 때까지 대기
            while (PhotonNetwork.IsConnected)
            {
                yield return null;
            }
        }
        //yield return new WaitForSeconds(0.2f);
        //StartCoroutine(ServerColor());
    }



}
