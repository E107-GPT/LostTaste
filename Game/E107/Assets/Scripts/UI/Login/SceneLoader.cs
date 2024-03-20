using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Camp Scene으로 전환하는 컴포넌트입니다.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    // Camp Scene을 로드하는 메서드
    public void LoadCampScene()
    {
        SceneManager.LoadScene("Camp");
    }
}
