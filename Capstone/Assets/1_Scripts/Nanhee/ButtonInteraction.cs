using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    PhotonView photonView;

    public GameObject interactionUI; // ��ȣ�ۿ� UI
    private GameObject currentInteractable; // ���� ��ȣ�ۿ� ������ ������Ʈ
    public Button button;
    private StageClear stageclear;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();

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
        if (interactionUI != null)
        {
            interactionUI.SetActive(false); // ó���� UI�� ��Ȱ��ȭ
        }
    }

    [PunRPC]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            if (interactionUI != null)
            {
                interactionUI.SetActive(true); // ��ȣ�ۿ� UI Ȱ��ȭ
            }

            // ���� �ش� ������Ʈ�� CorrectNumber �±׸� ������ �ִٸ� currentInteractable�� ����
            if (other.CompareTag("CorrectNumber"))
            {
                currentInteractable = other.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            if (interactionUI != null)
            {
                interactionUI.SetActive(false); // ��ȣ�ۿ� UI ��Ȱ��ȭ
            }

            // ���� ��ȣ�ۿ� ������Ʈ�� ������ ������Ʈ�� ������ ��� ����
            if (currentInteractable == other.gameObject)
            {
                currentInteractable = null;
            }
        }
    }

    public void OnClickButton()
    {
        stageclear = GameObject.Find("StageClear").GetComponent<StageClear>();

        // currentInteractable�� CorrectNumber �±׸� ������ �ִ� ��쿡�� stage3clear ����
        if (currentInteractable != null && currentInteractable.CompareTag("CorrectNumber"))
        {
            if (stageclear != null)
            {
                stageclear.stage3clear = true;
            }
        }
        else
        {
            // �߸��� ��ȣ�ۿ��� �� ��ư�� ���������� ����
            if (button != null && button.targetGraphic != null)
            {
                StartCoroutine(ChangeButtonColorTemporarily(Color.red, 3f));
            }
        }
    }

    private IEnumerator ChangeButtonColorTemporarily(Color color, float duration)
    {
        // ���� ������ ����
        Color originalColor = button.targetGraphic.color;
        // ������ ����
        button.targetGraphic.color = color;
        // duration��ŭ ���
        yield return new WaitForSeconds(duration);
        // ���� �������� ����
        button.targetGraphic.color = originalColor;
    }
}
