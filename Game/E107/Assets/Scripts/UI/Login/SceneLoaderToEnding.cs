using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// Dungeon Scene으로 전환하는 컴포넌트입니다.
/// </summary>
public class SceneLoaderToEnding : MonoBehaviour
{
    // Camp Scene을 로드하는 메서드
    public void LoadEndingScene()
    {
        HTTPRequest request;
        request = GameObject.Find("gm").GetComponent<HTTPRequest>();
        string roomName =  PhotonNetwork.CurrentRoom.Name;
        int memberCount = PhotonNetwork.CurrentRoom.PlayerCount;
        
        string printRoomName = roomName;
        int lastIndex = printRoomName.LastIndexOf("`");
        if (lastIndex != -1)
            printRoomName = printRoomName.Substring(0, lastIndex);

        float time = GameObject.Find("Portal-ForestEntrance-Spawn").GetComponent<DungeonEntrance>().GameTime;

        Debug.Log(time);

        // 로그인 요청 보내기
        Dictionary<string, string> requestParam = new Dictionary<string, string>();
        requestParam.Add("partyName", printRoomName);
        requestParam.Add("memberCount", memberCount.ToString());
        requestParam.Add("playTime", time.ToString());
        requestParam.Add("rngSeed", PhotonNetwork.CurrentRoom.CustomProperties["seed"].ToString());

        Debug.Log(requestParam);
        request.POSTCall("adventure", requestParam);


        Managers.Sound.Clear();
        SceneManager.LoadScene("Ending");
    }
}
