using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CharacterTransition : MonoBehaviour
{
    public GameObject oldCharacter; // 기존 캐릭터
    public GameObject newCharacterPrefab; // 새 캐릭터 프리팹
    public Vector3 spawnPosition; // 새 캐릭터의 스폰 위치
    public int newLayer; // 새 캐릭터에 적용될 레이어
    public PostProcessVolume postProcessVolume; // Vignette가 적용된 PostProcessVolume
    public float vignetteTargetIntensity = 1.0f; // 목표 Vignette intensity
    public float vignetteSpeed = 0.02f; // Vignette intensity가 증가하는 속도
    public float vignettesmoothness = 1.0f;

    private Vignette vignette;
    private float currentTime = 0f;
    private bool transitionStarted = false;

    private void Start()
    {
        // PostProcessVolume에서 Vignette 효과 가져오기
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = 0f; // 초기 Vignette intensity 설정
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 19f && !transitionStarted)
        {
            transitionStarted = true;
            StartTransition();
        }

        // Vignette intensity가 점진적으로 증가하도록 설정
        if (transitionStarted && vignette != null && vignette.intensity.value < vignetteTargetIntensity)
        {
            vignette.smoothness.value = vignettesmoothness;
            vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, vignetteTargetIntensity, vignetteSpeed * Time.deltaTime);
        }
    }

    private void StartTransition()
    {
        // 기존 캐릭터 삭제
        Destroy(oldCharacter);

        // 새로운 캐릭터 생성 및 레이어 변경
        GameObject newCharacter = Instantiate(newCharacterPrefab, spawnPosition, Quaternion.identity);
        Camera cameraInNewCharacter = newCharacter.GetComponentInChildren<Camera>();
        if (cameraInNewCharacter != null)
        {
            cameraInNewCharacter.gameObject.layer = newLayer;
        }
        else
        {
            Debug.LogWarning("새 캐릭터에 카메라가 존재하지 않습니다.");
        }

       
    }





}
