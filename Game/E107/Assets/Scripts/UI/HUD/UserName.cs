using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserName : MonoBehaviour
{
    public TextMeshProUGUI userNameText;

    void Start()
    {
        // UserInfo 인스턴스 가져오기
        UserInfo userInfo = UserInfo.GetInstance();

        // 닉네임 정보 가져오기
        string nickname = userInfo.getNickName();

        // 가져온 정보를 TextMeshProUGUI에 적용
        userNameText.text = nickname;
    }

}
