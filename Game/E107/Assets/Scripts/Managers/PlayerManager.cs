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

        // ���濡�� �÷��̾� ���� �޾ƿͼ� �־������
        // �ʱ� ����(���� ����)�� �޾ƿ;� �Ѵ�.

        //_currentPlayerNumber = SetLocalPlayerInfo(Define.ClassType.Warrior);
    }


    // ���� ���� ���� ������ �����ɴϴ�.
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
                // ���⿡�� �÷��̾��� ���� ���� ���� �߰��� ó���� �� �ֽ��ϴ�.

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

                // ���� Ŀ���� ������Ƽ ������Ʈ
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

        // ���� Ŀ���� ������Ƽ ������Ʈ
        usedNumbers[myNumber] = 1; // ��ȣ ��� ǥ��
        var roomProperties = new ExitGames.Client.Photon.Hashtable { { "UsedNumbers", usedNumbers } };
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);
    }

    int FindAvailableNumber(int[] usedNumbers)
    {
        for (int i = 0; i < usedNumbers.Length; i++)
        {
            if (usedNumbers[i] == 0) // ������ ���� ��ȣ ã��
                return i;
        }
        return -1; // ��� ��ȣ�� ��� ��
    }

    // ���� �ٲ� �� ������Ʈ ���Ѿ���
    public void UpdateLocalPlayerInfo(Define.ClassType type)
    {
        Player player = _playerList[_currentPlayerNumber];
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
        {
            { "Number", _currentPlayerNumber}, // ��ȣ ����
            { "Class", type }  // ���� ����
        };
        player.SetCustomProperties(props);
    }



    // �ٸ� ����� �濡�� ������?
    public void DeletePlayerInfo(int num)
    {
        //_playerList[num].CustomProperties = null;
        _playerList[num] = null;
        
    }


    // _playerList ���� ����� ã��
    private int FindBlankNumber()
    {
        for(int i = 0; i < 4; i++)
        {
            if (_playerList[i] == null) return i;
        }

        return -1;
    }


    // �濡�� ������ �� ����
    public void Clear()
    {
        // �泪���� Ŭ���� �������
        for(int i = 0; i < 4; i++)
        {
            DeletePlayerInfo(i);
        }

    }

}
