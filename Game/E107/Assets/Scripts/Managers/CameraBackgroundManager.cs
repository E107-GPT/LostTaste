using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraBackgroundManager : MonoBehaviour
{
    public Camera mainCamera; // ���� ī�޶�
    public Material[] skyboxes; // Skybox�� ����� Material �迭
    public Color[] backgroundColors; // Solid Color�� ����� ���� �迭

    void Start()
    {
        skyboxes = Resources.LoadAll<Material>("Material/Backgrounds");
    }

    // Skybox ����
    public void ChangeSkybox(int index)
    {
        if (index < 0 || index >= skyboxes.Length) return;

        RenderSettings.skybox = skyboxes[index];
        DynamicGI.UpdateEnvironment(); // ȯ�� ������ ������Ʈ
    }

    // ��� ���� ����
    public void ChangeBackgroundColor(int index)
    {
        if (index < 0 || index >= backgroundColors.Length) return;

        mainCamera.backgroundColor = backgroundColors[index];
    }

}
