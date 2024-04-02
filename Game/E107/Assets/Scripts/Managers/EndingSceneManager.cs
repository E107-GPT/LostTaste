using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneManager : MonoBehaviour
{
    public GameObject skipButton;
    private AsyncOperation asyncLoad; // 비동기 씬 로딩을 위한 AsyncOperation
    private bool storyComplete = false; // 스토리 완료 체크

    void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
        skipButton.SetActive(false); // 스킵 버튼 비활성화
    }

    IEnumerator LoadYourAsyncScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync("Dungeon"); // 던전 씬 로딩
        asyncLoad.allowSceneActivation = false; // 자동 씬 전환 방지

        // 로딩이 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 진행률 계산

            // 로딩 완료 시 스킵 버튼 활성화
            if (asyncLoad.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2);
                skipButton.SetActive(true);
            }

            yield return null;
        }
    }

    // 스토리 완
    public void CompleteStory()
    {
        storyComplete = true;
        // 스토리가 완료되고 로딩도 완료된 경우 자동으로 씬 전환
        if (asyncLoad.progress >= 0.9f)
        {
            asyncLoad.allowSceneActivation = true;
        }
    }

    // 스킵 버튼 클릭 시 씬 전환
    public void OnSkipButtonClicked()
    {
        // 씬 전환을 즉시 허용합니다.
        asyncLoad.allowSceneActivation = true;
    }
}
