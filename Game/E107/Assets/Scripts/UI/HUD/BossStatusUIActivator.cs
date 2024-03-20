using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �÷��̾ ���� �濡 �����ϸ� ���� ���� UI�� Ȱ��ȭ�ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class BossStatusUIActivator : MonoBehaviour
{
    // ���� ���� �г�
    [Header("[ ���� ���� �г� ]")]
    public GameObject bossStatusPanel; // ���� ���� �г�

    // �÷��̾ ���� �濡 ������ �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossStatusPanel.SetActive(true); // ���� ü�� UI Ȱ��ȭ
        }
    }
}