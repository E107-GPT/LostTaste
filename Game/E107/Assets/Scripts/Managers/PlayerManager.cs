using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{

    Player[] _playerList = new Player[4];
    int _currentPlayerNumber = -1;

    public void Init()
    {
        _playerList = new Player[4];

        // 포톤에서 플레이어 정보 받아와서 넣어줘야함
        // 초기 정보(직업 정보)도 받아와야 한다.
        // 
        _currentPlayerNumber = SetLocalPlayerInfo(Define.ClassType.Warrior);
    }


    void LoadPlayersInfoInCurrentRoom()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            Player player = playerInfo.Value;
            object numObj;
            player.CustomProperties.TryGetValue("Number", out numObj);
            int num = (int)numObj;
            _playerList[num] = player;
        }
        
    }

    
    public int SetLocalPlayerInfo(Define.ClassType type)
    {
        Player player = PhotonNetwork.LocalPlayer;

        int num = FindBlankNumber();
        if(num == -1)
        {
            Debug.LogError("플레이어가 들어갈 공간이 없어요.");
            return num;
        }
        // Number를 찾아주자
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
        {
            { "Number", num}, // 번호 지정
            { "Class", type }  // 직업 지정
        };

        player.SetCustomProperties(props);
        _playerList[num] = player;

        return num;

        
    }

    // 직업 바꿀 떄 업데이트 시켜야함
    public void UpdateLocalPlayerInfo(Define.ClassType type)
    {
        Player player = _playerList[_currentPlayerNumber];
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
        {
            { "Number", _currentPlayerNumber}, // 번호 지정
            { "Class", type }  // 직업 지정
        };
        player.SetCustomProperties(props);
    }



    // 다른 사람이 방에서 나가면?
    void DeleteLocalPlayerInfo(int num)
    {
        //_playerList[num].CustomProperties = null;
        _playerList[num] = null;
        
    }


    // _playerList 에서 빈공간 찾기
    private int FindBlankNumber()
    {
        for(int i = 0; i < 4; i++)
        {
            if (_playerList[i] == null) return i;
        }

        return -1;
    }


    // 방에서 나갔을 때 실행
    void Clear()
    {
        // 방나갈떄 클리어 해줘야함
        for(int i = 0; i < 4; i++)
        {
            DeleteLocalPlayerInfo(i);
        }

    }

}
