using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisappear : MonoBehaviour
{
    public float fadeDuration = 2.0f; // 사라지는데 걸리는 시간

    void Start()
    {
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Color startColor = meshRenderer.material.color;
        float time = 0;

        while (time < fadeDuration)
        {
            // 경과 시간에 따라 투명도를 조절합니다.
            float alpha = Mathf.Lerp(1.0f, 0.0f, time / fadeDuration);
            meshRenderer.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            // 다음 프레임까지 기다린 후, 시간을 업데이트합니다.
            time += Time.deltaTime;
            yield return null;
        }

        // 최종적으로 오브젝트를 완전히 투명하게 만듭니다.
        meshRenderer.material.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
        Destroy(gameObject);
    }
}
