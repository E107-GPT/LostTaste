using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{

    public Player[] _playerList = new Player[4];
    int _currentPlayerNumber = -1;

    public void Init()
    {
        _playerList = new Player[4];

        // 포톤에서 플레이어 정보 받아와서 넣어줘야함
        // 초기 정보(직업 정보)도 받아와야 한다.

        //_currentPlayerNumber = SetLocalPlayerInfo(Define.ClassType.Warrior);
    }


    // 현재 방의 유저 정보를 가져옵니다.
    public void LoadPlayersInfoInCurrentRoom()
    {
        //foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        //{
        //    Player player = playerInfo.Value;
        //    Debug.Log(player.CustomProperties);
        //    object numObj;
        //    if(player.CustomProperties.TryGetValue("Number", out numObj))
        //    {
        //        Debug.Log(numObj);
        //        int num = (int)numObj;
        //        _playerList[num] = player;
        //    }

        //}
        foreach (var player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player.CustomProperties.TryGetValue("Number", out object numberObj))
            {
                int number = (int)numberObj;
                _playerList[number] = player;
                // 여기에서 플레이어의 직업 정보 등을 추가로 처리할 수 있습니다.

                Debug.Log($"{player}   {number}");
            }
        }
    }

    public void AddPlayer(Player newPlayer)
    {
        if (newPlayer.CustomProperties.TryGetValue("Number", out object numberObj))
        {
            int number = (int)numberObj;
            _playerList[number] = newPlayer;
        }
    }

    public void RemovePlayer(Player leftPlayer)
    {
        if (leftPlayer.CustomProperties.TryGetValue("Number", out object numberObj))
        {
            int number = (int)numberObj;
            
            var usedNumbers = PhotonNetwork.CurrentRoom.CustomProperties["UsedNumbers"] as int[];
            if (usedNumbers != null && number >= 0 && number < usedNumbers.Length)
            {
                usedNumbers[number] = 0;

                // 방의 커스텀 프로퍼티 업데이트
                var roomProperties = new ExitGames.Client.Photon.Hashtable { { "UsedNumbers", usedNumbers } };
                PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
                _playerList[number] = null;
            }


        }
    }


    public void SetLocalPlayerInfo(Define.ClassType type)
    {
        var usedNumbers = PhotonNetwork.CurrentRoom.CustomProperties["UsedNumbers"] as int[] ?? new int[4];
        int myNumber = FindAvailableNumber(usedNumbers);
        var myProperties = new ExitGames.Client.Photon.Hashtable { { "Number", myNumber }, { "Class", type } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(myProperties);

        // 방의 커스텀 프로퍼티 업데이트
        usedNumbers[myNumber] = 1; // 번호 사용 표시
        var roomProperties = new ExitGames.Client.Photon.Hashtable { { "UsedNumbers", usedNumbers } };
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
    }

    int FindAvailableNumber(int[] usedNumbers)
    {
        for (int i = 0; i < usedNumbers.Length; i++)
        {
            if (usedNumbers[i] == 0) // 사용되지 않은 번호 찾기
                return i;
        }
        return -1; // 모든 번호가 사용 중
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
    public void DeletePlayerInfo(int num)
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
    public void Clear()
    {
        // 방나갈떄 클리어 해줘야함
        for(int i = 0; i < 4; i++)
        {
            DeletePlayerInfo(i);
        }

    }

}
