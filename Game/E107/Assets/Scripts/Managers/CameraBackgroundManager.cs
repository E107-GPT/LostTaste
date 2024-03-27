using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraBackgroundManager : MonoBehaviour
{
    public Camera mainCamera; // 메인 카메라
    public Material[] skyboxes; // Skybox로 사용할 Material 배열
    public Color[] backgroundColors; // Solid Color로 사용할 색상 배열

    void Start()
    {
        skyboxes = Resources.LoadAll<Material>("Material/Backgrounds");
    }

    // Skybox 변경
    public void ChangeSkybox(int index)
    {
        if (index < 0 || index >= skyboxes.Length) return;

        RenderSettings.skybox = skyboxes[index];
        DynamicGI.UpdateEnvironment(); // 환경 라이팅 업데이트
    }

    // 배경 색상 변경
    public void ChangeBackgroundColor(int index)
    {
        if (index < 0 || index >= backgroundColors.Length) return;

        mainCamera.backgroundColor = backgroundColors[index];
    }

}
