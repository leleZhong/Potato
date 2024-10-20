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
    [SerializeField] private StageClear stageClear; // StageClear ����

    GameObject mjUI;

    void Awake()
    {
        // MJ_UI ������Ʈ �ڵ����� �Ҵ�
        mjUI = GameObject.Find("MJ_UI");
        if (mjUI != null)
        {
            // TextMeshPro UI ��� �ڵ� �Ҵ�
            countDisplayUI = mjUI.transform.Find("countDisplayUI").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("MJ_UI ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    void Start()
    {
        // ColorChanger ������Ʈ �ڵ� �Ҵ�
        colorChanger = FindObjectOfType<ColorChanger>();
        if (colorChanger == null)
        {
            Debug.LogError("ColorChanger ������Ʈ�� ã�� �� �����ϴ�.");
        }

        // StageClear ������Ʈ �ڵ� �Ҵ�
        stageClear = FindObjectOfType<StageClear>();
        if (stageClear == null)
        {
            Debug.LogError("StageClear ������Ʈ�� ã�� �� �����ϴ�.");
        }

        // MJ_UI ��ü�� ��Ȱ��ȭ
        if (mjUI != null)
        {
            mjUI.SetActive(false);
        }
    }

    void Update()
    {
        // �ڽ� ������Ʈ �� "Sphere"�� �־������ ������ ����
        Transform existingSphere = transform.Find("Sphere");
        if (existingSphere == null)
        {
            // Debug.Log("Sphere ������Ʈ�� �����ϴ�.");
            return; // Sphere�� ������ Update �Լ� ����
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            IncrementColorCount();
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            ResetCounts();
        }

        // ���� ī��Ʈ�� ������ �����ϴ��� Ȯ���ϴ� �Լ� ȣ��
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

        countDisplayUI.text = GetCountDisplayText(); // ī��Ʈ ���ŵ� �ؽ�Ʈ UI ǥ��
        Debug.Log(currentColor + " ������ ī��Ʈ�� �����߽��ϴ�.");
    }

    void ResetCounts()
    {
        redCount = 0;
        blueCount = 0;
        yellowCount = 0;
        greenCount = 0;

        countDisplayUI.text = GetCountDisplayText();
        Debug.Log("ī��Ʈ�� �ʱ�ȭ�Ǿ����ϴ�.");
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

            // MJ_UI ��ü Ȱ��ȭ
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

            // �� �浹�� ��ġ�� ��Ȱ��ȭ MJ_UI ��ü ��Ȱ��ȭ
            if (mjUI != null)
            {
                mjUI.SetActive(false);
            }
        }
    }

    // Ŭ���� ������ üũ�ϴ� �Լ�
    private void CheckStageClearCondition()
    {
        // Ŭ���� ���� ��Ȯ�� ��ġ ��
        if (redCount == redClearCount && blueCount == blueClearCount && yellowCount == yellowClearCount && greenCount == greenClearCount)
        {
            if (stageClear != null)
            {
                stageClear.stage2clear = true; // Ŭ���� ������ �����Ǹ� stage2clear�� true�� ����
                Transform existingSphere = transform.Find("Sphere");
                if (existingSphere != null)
                {
                    Destroy(existingSphere.gameObject); // �ڽ� ������Ʈ ����
                    Debug.Log("���� Sphere ������Ʈ�� ���ŵǾ����ϴ�.");
                }
                else
                {
                    Debug.Log("������ Sphere ������Ʈ�� �����ϴ�.");
                }

                Debug.Log("�������� Ŭ����!");
            }

            // Ŭ���� �� ī��Ʈ �ʱ�ȭ
            ResetCounts();
        }

        // �ϳ��� Ŭ���� ���� �ʰ� �� ī��Ʈ �ʱ�ȭ
        if (redCount > redClearCount || blueCount > blueClearCount || yellowCount > yellowClearCount || greenCount > greenClearCount)
        {
            Debug.Log("ī��Ʈ�� Ŭ���� ������ �ʰ��Ͽ� �ʱ�ȭ�Ǿ����ϴ�.");
            ResetCounts();
        }
    }
}
