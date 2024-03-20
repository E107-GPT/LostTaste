using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �����ϴ� ������Ʈ�Դϴ�.
/// �����Ϳ��� ���� ���� ��� UnityEditor�� �̿��Ͽ� �����ϰ�,
/// �����Ͱ� �ƴ� ��� Application.Quit()�� ȣ���Ͽ� ���ø����̼��� �����մϴ�.
/// </summary>
public class GameExitController : MonoBehaviour
{
    // ������ �����ϴ� �޼���
    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }
}
