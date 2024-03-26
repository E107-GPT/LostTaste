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

        // ���濡�� �÷��̾� ���� �޾ƿͼ� �־������
        // �ʱ� ����(���� ����)�� �޾ƿ;� �Ѵ�.
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
            Debug.LogError("�÷��̾ �� ������ �����.");
            return num;
        }
        // Number�� ã������
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
        {
            { "Number", num}, // ��ȣ ����
            { "Class", type }  // ���� ����
        };

        player.SetCustomProperties(props);
        _playerList[num] = player;

        return num;

        
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
    void DeleteLocalPlayerInfo(int num)
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
    void Clear()
    {
        // �泪���� Ŭ���� �������
        for(int i = 0; i < 4; i++)
        {
            DeleteLocalPlayerInfo(i);
        }

    }

}
