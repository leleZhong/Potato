using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class VignetteFader : MonoBehaviour
{
    public Transform player; // 플레이어의 위치
    public Vector3 targetPosition; // 목표 위치
    public float activationDistance = 5f; // Vignette 효과가 활성화될 거리
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

    void Update()
    {
        float distance = Vector3.Distance(player.position, targetPosition);

        // 플레이어가 목표 위치에 도달하면 Vignette 효과를 활성화
        if (distance <= activationDistance && !isVignetteActive)
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
