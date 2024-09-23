using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class VignetteFader : MonoBehaviour
{
    public Transform player; // 플레이어의 위치
    public Transform targetObject; // Vignette 효과를 활성화할 목표 물체
    public PostProcessVolume postProcessVolume; // 연결된 Post Process Volume
    private Vignette vignette;
    public float fadeDuration; // Vignette 효과가 점점 줄어드는 데 걸리는 시간
    public float targetIntensity; // 목표 Vignette Intensity 값

    private bool isVignetteActive = false;

    void Start()
    {
        // Vignette 효과 설정 가져오기
        postProcessVolume.profile.TryGetSettings(out vignette);
        vignette.intensity.value = 0f; // 초기 Vignette Intensity 값을 0으로 설정 (시야 넓게)
    }

    void OnTriggerEnter(Collider other)
    {
        // 특정 물체와 플레이어가 접촉할 때 Vignette 효과를 활성화
        if (other.transform == targetObject && !isVignetteActive)
        {
            StartCoroutine(FadeInVignette());
            isVignetteActive = true; // 중복 실행 방지
        }
    }

    IEnumerator FadeInVignette()
    {
        float elapsedTime = 0f;
        float startIntensity = vignette.intensity.value;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / fadeDuration);
            yield return null;
        }

        vignette.intensity.value = targetIntensity;
    }
}
