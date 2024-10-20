using TMPro;
using UnityEngine;

public class ColorCounter : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private string currentTag = "";

    [SerializeField] private int redCount = 0;
    [SerializeField] private int blueCount = 0;
    [SerializeField] private int yellowCount = 0;
    [SerializeField] private int greenCount = 0;

    [SerializeField] private int redClearCount = 11;
    [SerializeField] private int greenClearCount = 11;
    [SerializeField] private int yellowClearCount = 7;
    [SerializeField] private int blueClearCount = 13;


    [SerializeField] private Transform cameraTransform;

    [SerializeField] private TextMeshProUGUI countDisplayUI;

    [SerializeField] private ColorChanger colorChanger;
    [SerializeField] private StageClear stageClear; // StageClear 참조

    GameObject mjUI;

    void Awake()
    {
        // MJ_UI 오브젝트 자동으로 할당
        mjUI = GameObject.Find("MJ_UI");
        if (mjUI != null)
        {
            // TextMeshPro UI 요소 자동 할당
            countDisplayUI = mjUI.transform.Find("countDisplayUI").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("MJ_UI 오브젝트를 찾을 수 없습니다.");
        }
    }

    void Start()
    {
        // ColorChanger 오브젝트 자동 할당
        colorChanger = FindObjectOfType<ColorChanger>();
        if (colorChanger == null)
        {
            Debug.LogError("ColorChanger 오브젝트를 찾을 수 없습니다.");
        }

        // StageClear 오브젝트 자동 할당
        stageClear = FindObjectOfType<StageClear>();
        if (stageClear == null)
        {
            Debug.LogError("StageClear 오브젝트를 찾을 수 없습니다.");
        }

        // MJ_UI 전체를 비활성화
        if (mjUI != null)
        {
            mjUI.SetActive(false);
        }
    }

    void Update()
    {
        // 자식 오브젝트 중 "Sphere"가 있어야지만 조건이 동작
        Transform existingSphere = transform.Find("Sphere");
        if (existingSphere == null)
        {
            // Debug.Log("Sphere 오브젝트가 없습니다.");
            return; // Sphere가 없으면 Update 함수 종료
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            IncrementColorCount();
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            ResetCounts();
        }

        // 색상 카운트가 조건을 충족하는지 확인하는 함수 호출
        CheckStageClearCondition();
    }

    void IncrementColorCount()
    {
        if (colorChanger == null) return;

        Color currentColor = colorChanger.GetCurrentColor();

        if (currentTag == "red" && currentColor == Color.red)
            redCount++;
        else if (currentTag == "blue" && currentColor == Color.blue)
            blueCount++;
        else if (currentTag == "yellow" && currentColor == Color.yellow)
            yellowCount++;
        else if (currentTag == "green" && currentColor == Color.green)
            greenCount++;

        countDisplayUI.text = GetCountDisplayText(); // 카운트 갱신된 텍스트 UI 표시
        Debug.Log(currentColor + " 색상의 카운트가 증가했습니다.");
    }

    void ResetCounts()
    {
        redCount = 0;
        blueCount = 0;
        yellowCount = 0;
        greenCount = 0;

        countDisplayUI.text = GetCountDisplayText();
        Debug.Log("카운트가 초기화되었습니다.");
    }

    private string GetCountDisplayText()
    {
        return $"Red: {redCount}, Blue: {blueCount}, Yellow: {yellowCount}, Green: {greenCount}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("red") || other.CompareTag("blue") || other.CompareTag("yellow") || other.CompareTag("green"))
        {
            isPlayerInRange = true;
            currentTag = other.tag;

            // MJ_UI 전체 활성화
            if (mjUI != null)
            {
                mjUI.SetActive(true);
            }

            countDisplayUI.text = GetCountDisplayText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(currentTag))
        {
            isPlayerInRange = false;
            currentTag = "";

            // 씬 충돌시 겹치는 비활성화 MJ_UI 전체 비활성화
            if (mjUI != null)
            {
                mjUI.SetActive(false);
            }
        }
    }

    // 클리어 조건을 체크하는 함수
    private void CheckStageClearCondition()
    {
        // 클리어 조건 정확히 일치 시
        if (redCount == redClearCount && blueCount == blueClearCount && yellowCount == yellowClearCount && greenCount == greenClearCount)
        {
            if (stageClear != null)
            {
                stageClear.stage2clear = true; // 클리어 조건이 충족되면 stage2clear를 true로 설정
                Transform existingSphere = transform.Find("Sphere");
                if (existingSphere != null)
                {
                    Destroy(existingSphere.gameObject); // 자식 오브젝트 제거
                    Debug.Log("기존 Sphere 오브젝트가 제거되었습니다.");
                }
                else
                {
                    Debug.Log("제거할 Sphere 오브젝트가 없습니다.");
                }

                Debug.Log("스테이지 클리어!");
            }

            // 클리어 후 카운트 초기화
            ResetCounts();
        }

        // 하나라도 클리어 조건 초과 시 카운트 초기화
        if (redCount > redClearCount || blueCount > blueClearCount || yellowCount > yellowClearCount || greenCount > greenClearCount)
        {
            Debug.Log("카운트가 클리어 조건을 초과하여 초기화되었습니다.");
            ResetCounts();
        }
    }
}
