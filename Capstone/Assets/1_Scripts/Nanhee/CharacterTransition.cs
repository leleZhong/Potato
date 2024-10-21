using Photon.Pun;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CharacterTransition : MonoBehaviour
{
    public GameObject oldCharacter; // ���� ĳ����
    // public GameObject newCharacterPrefab; // �� ĳ���� ������
    // public Vector3 spawnPosition; // �� ĳ������ ���� ��ġ
    public int newLayer; // �� ĳ���Ϳ� ����� ���̾�
    public PostProcessVolume postProcessVolume; // Vignette�� ����� PostProcessVolume
    public float vignetteTargetIntensity = 1.0f; // ��ǥ Vignette intensity
    public float vignetteSpeed = 0.02f; // Vignette intensity�� �����ϴ� �ӵ�
    public float vignettesmoothness = 1.0f;

    private Vignette vignette;
    private float currentTime = 0f;
    private bool transitionStarted = false;

    // TutorialManager ����
    public TutorialManager tutorialManager;
    public PhotonView _pv;
    public Camera _camera;

    private void Start()
    {
        // PostProcessVolume���� Vignette ȿ�� ��������
        if (postProcessVolume.profile.TryGetSettings(out vignette))
        {
            vignette.intensity.value = 0f; // �ʱ� Vignette intensity ����
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 19f && !transitionStarted)
        {
            StartTransition();
            SetCameraLayer();
        }

        // Vignette intensity�� ���������� �����ϵ��� ����
        if (transitionStarted && vignette != null && vignette.intensity.value < vignetteTargetIntensity)
        {
            vignette.smoothness.value = vignettesmoothness;
            vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, vignetteTargetIntensity, vignetteSpeed * Time.deltaTime);
        }
    }

    private void StartTransition()
    {
        // ���� ĳ���� ����
        Destroy(oldCharacter);

        // // ���ο� ĳ���� ���� �� ���̾� ����
        // GameObject newCharacter = Instantiate(newCharacterPrefab, spawnPosition, Quaternion.identity);
        // Camera cameraInNewCharacter = newCharacter.GetComponentInChildren<Camera>();
        // if (cameraInNewCharacter != null)
        // {
        //     cameraInNewCharacter.gameObject.layer = newLayer;
        // }
        // else
        // {
        //     Debug.LogWarning("�� ĳ���Ϳ� ī�޶� �������� �ʽ��ϴ�.");
        // }

        // TutorialManager���� ĳ���� ���� ó��
        if (tutorialManager != null)
            tutorialManager.SpawnPlayers();
        else
            Debug.Log("TutorialManager�� �Ҵ��ؾ� �մϴ�.");

        if (vignette != null)
        {
            vignette.intensity.value = 0f;  // Vignette �ʱ�ȭ
            transitionStarted = true;  // Vignette ȿ�� ���� ����
        }
    }

    void SetCameraLayer()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in allPlayers)
        {
            _pv = player.GetComponent<PhotonView>();
            _camera = player.GetComponentInChildren<Camera>();

            if (_pv.IsMine)
                _camera.gameObject.layer = newLayer;
        }
    }
}
