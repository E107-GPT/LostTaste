using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Dungeon Scene으로 전환하는 컴포넌트입니다.
/// </summary>
public class SceneLoaderToEnding : MonoBehaviour
{
    // Camp Scene을 로드하는 메서드
    public void LoadEndingScene()
    {
        Managers.Sound.Clear();
        SceneManager.LoadScene("Ending");
    }
}
