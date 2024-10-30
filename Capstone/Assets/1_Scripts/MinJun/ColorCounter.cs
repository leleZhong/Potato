using TMPro;
using Photon.Pun;
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
    [SerializeField] private TextMeshProUGUI interactTextUI;
    [SerializeField] private TextMeshProUGUI resetTextUI;

    [SerializeField] private ColorChanger colorChanger;
    [SerializeField] private StageClear stageClear;

    GameObject mjUI;
    public PhotonView photonView;

    void Awake()
    {
        // MJ_UI ������Ʈ �ڵ����� �Ҵ�
        mjUI = GameObject.Find("MJ_UI");
        if (mjUI != null)
        {
            // TextMeshPro UI ��� �ڵ� �Ҵ�
            countDisplayUI = mjUI.transform.Find("countDisplayUI").GetComponent<TextMeshProUGUI>();
            interactTextUI = mjUI.transform.Find("interactTextUI").GetComponent<TextMeshProUGUI>();
            resetTextUI = mjUI.transform.Find("resetTextUI").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("MJ_UI ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    void Start()
    {
        colorChanger = FindObjectOfType<ColorChanger>();
        if (colorChanger == null)
        {
            Debug.LogError("ColorChanger ������Ʈ�� ã�� �� �����ϴ�.");
        }

        stageClear = FindObjectOfType<StageClear>();
        if (stageClear == null)
        {
            Debug.LogError("StageClear ������Ʈ�� ã�� �� �����ϴ�.");
        }

        if (mjUI != null)
        {
            mjUI.SetActive(false);
        }
    }

    void Update()
    {
        Transform existingSphere = transform.Find("Sphere");
        if (existingSphere == null)
        {
            return;
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            IncrementColorCount();
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            ResetCounts();
        }

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

        countDisplayUI.text = GetCountDisplayText();
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

            // �� ���� �ؽ�Ʈ UI�� ������ �����Ͽ� ȭ�鿡 ǥ��
            if (countDisplayUI != null)
            {
                countDisplayUI.text = GetCountDisplayText();
            }
            if (interactTextUI != null)
            {
                interactTextUI.text = "Press E to increment the color count";
            }
            if (resetTextUI != null)
            {
                resetTextUI.text = "Press R to reset the counts";
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(currentTag))
        {
            isPlayerInRange = false;
            currentTag = "";

            // �� ���� �ؽ�Ʈ ��Ҹ� ��� ����
            if (countDisplayUI != null)
            {
                countDisplayUI.text = "";
            }
            if (interactTextUI != null)
            {
                interactTextUI.text = "";
            }
            if (resetTextUI != null)
            {
                resetTextUI.text = "";
            }
        }
    }

    private void CheckStageClearCondition()
    {
        if (redCount == redClearCount && blueCount == blueClearCount && yellowCount == yellowClearCount && greenCount == greenClearCount)
        {
            if (stageClear != null)
            {
                photonView.RPC("StageClearRPC", RpcTarget.All);
                Debug.Log("�������� Ŭ����!");
            }
            ResetCounts();
        }

        if (redCount > redClearCount || blueCount > blueClearCount || yellowCount > yellowClearCount || greenCount > greenClearCount)
        {
            Debug.Log("ī��Ʈ�� Ŭ���� ������ �ʰ��Ͽ� �ʱ�ȭ�Ǿ����ϴ�.");
            ResetCounts();
        }
    }

    [PunRPC]
    private void StageClearRPC()
    {
        stageClear.stage2clear = true;

        Transform existingSphere = transform.Find("Sphere");
        if (existingSphere != null)
        {
            Destroy(existingSphere.gameObject);
            Debug.Log("���� Sphere ������Ʈ�� ���ŵǾ����ϴ�.");
        }
        else
        {
            Debug.Log("������ Sphere ������Ʈ�� �����ϴ�.");
        }
    }
}
