using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Camp Scene���� ��ȯ�ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    // Camp Scene�� �ε��ϴ� �޼���
    public void LoadCampScene()
    {
        SceneManager.LoadScene("Camp");
    }
}
