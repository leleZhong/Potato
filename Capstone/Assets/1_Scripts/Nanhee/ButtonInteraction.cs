using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    public PhotonView photonView;
    public GameObject interactionUI; // ��ȣ�ۿ� UI
    private GameObject currentInteractable; // ���� ��ȣ�ۿ� ������ ������Ʈ
    public Button button;
    public StageClear stageclear;

    void Awake()
    {
        // InteractionUI ������Ʈ ã��
        interactionUI = GameObject.Find("InteractionUI");

        if (interactionUI != null)
        {
            // InteractionUI ������ "Panel/InteractionButton" ������Ʈ ã��
            Transform buttonTransform = interactionUI.transform.Find("Panel/InteractionButton");

            if (buttonTransform != null)
            {
                button = buttonTransform.GetComponent<Button>();

                if (button != null)
                {
                    Debug.Log("Button ������Ʈ�� �ڵ����� �Ҵ�Ǿ����ϴ�: " + button.name);
                }
                else
                {
                    Debug.LogWarning("InteractionButton ������Ʈ�� Button ������Ʈ�� �����ϴ�.");
                }
            }
            else
            {
                Debug.LogWarning("Panel/InteractionButton ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("InteractionUI ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    void Start()
    {
        stageclear = FindObjectOfType<StageClear>();
        if (interactionUI != null)
        {
            interactionUI.SetActive(false); // ó���� UI�� ��Ȱ��ȭ
        }

        // ��ư�� OnClick �̺�Ʈ�� OnClickButton�� �Ҵ�
        SetupButton();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(true); // ��ȣ�ۿ� UI Ȱ��ȭ

            if (other.CompareTag("CorrectNumber"))
            {
                currentInteractable = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(false); // ��ȣ�ۿ� UI ��Ȱ��ȭ

            if (currentInteractable == other.gameObject)
            {
                currentInteractable = null;
            }
        }
    }

    public void OnClickButton()
    {
        if (currentInteractable != null && currentInteractable.CompareTag("CorrectNumber"))
        {
            if (stageclear != null)
            {
                photonView.RPC("SetStage3Clear", RpcTarget.All);
            }
        }
        else
        {
            if (button != null && button.targetGraphic != null)
            {
                StartCoroutine(ChangeButtonColorTemporarily(Color.red, 3f));
            }
        }
    }

    [PunRPC]
    void SetStage3Clear()
    {
        if (stageclear != null)
        {
            stageclear.stage3clear = true;
        }
    }

    private IEnumerator ChangeButtonColorTemporarily(Color color, float duration)
    {
        Color originalColor = button.targetGraphic.color;
        button.targetGraphic.color = color;
        yield return new WaitForSeconds(duration);
        button.targetGraphic.color = originalColor;
    }

    // ĳ���� ���� �� ��ư �̺�Ʈ�� �����ϴ� �޼���
    public void SetupButton()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners(); // ���� ������ ����
            button.onClick.AddListener(OnClickButton); // �� ������ �߰�
        }
    }
}
