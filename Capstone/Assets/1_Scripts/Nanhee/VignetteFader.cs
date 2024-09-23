using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class VignetteFader : MonoBehaviour
{
    public Transform player; // �÷��̾��� ��ġ
    public Transform targetObject; // Vignette ȿ���� Ȱ��ȭ�� ��ǥ ��ü
    public PostProcessVolume postProcessVolume; // ����� Post Process Volume
    private Vignette vignette;
    public float fadeDuration; // Vignette ȿ���� ���� �پ��� �� �ɸ��� �ð�
    public float targetIntensity; // ��ǥ Vignette Intensity ��

    private bool isVignetteActive = false;

    void Start()
    {
        // Vignette ȿ�� ���� ��������
        postProcessVolume.profile.TryGetSettings(out vignette);
        vignette.intensity.value = 0f; // �ʱ� Vignette Intensity ���� 0���� ���� (�þ� �а�)
    }

    void OnTriggerEnter(Collider other)
    {
        // Ư�� ��ü�� �÷��̾ ������ �� Vignette ȿ���� Ȱ��ȭ
        if (other.transform == targetObject && !isVignetteActive)
        {
            StartCoroutine(FadeInVignette());
            isVignetteActive = true; // �ߺ� ���� ����
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
