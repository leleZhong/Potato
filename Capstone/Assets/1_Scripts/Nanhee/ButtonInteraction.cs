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
    private GameObject triggeredObject;

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
        // BlockManager �ν��Ͻ� ã��
        BlockManager blockManager = FindObjectOfType<BlockManager>();
        if (blockManager != null && blockManager.correctBlock != null)
        {
            currentInteractable = blockManager.correctBlock;
            Debug.Log($"[ButtonInteraction] CorrectNumber ����� currentInteractable�� ������: {currentInteractable.name}");
        }

        // ���� �ʱ�ȭ ����
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
        Debug.Log($"[TriggerEnter] other.name: {other.gameObject.name}, �±�: {other.tag}");

        // �±װ� null�̰ų� ��� �ִ� ��� ���͸�
        if (string.IsNullOrEmpty(other.tag))
        {
            Debug.LogWarning($"[TriggerEnter] �±װ� Null�̰ų� ��� �ִ� ������Ʈ: {other.gameObject.name}");
            return; // ó�� �ߴ�
        }

        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(true);

            if (other.CompareTag("CorrectNumber"))
            {
                triggeredObject = other.gameObject;
                Debug.Log($"[CorrectNumber] triggeredObject ����: {triggeredObject.name}");
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"[TriggerExit] {other.gameObject.name}�� Trigger���� ���");

        if (string.IsNullOrEmpty(other.tag))
        {
            Debug.LogWarning($"[TriggerExit] �±װ� Null�̰ų� ��� �ִ� ������Ʈ: {other.gameObject.name}");
            return;
        }

        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(false); // ��ȣ�ۿ� UI ��Ȱ��ȭ

            if (triggeredObject == other.gameObject)
            {
                Debug.Log("[TriggerExit] triggeredObject �ʱ�ȭ");
                triggeredObject = null;
            }
        }
    }




    public void OnClickButton()
    {
        if (currentInteractable != null && triggeredObject != null)
        {
            Debug.Log($"[OnClickButton] currentInteractable: {currentInteractable.name}, �±�: {currentInteractable.tag}");
            Debug.Log($"[OnClickButton] triggeredObject: {triggeredObject.name}, �±�: {triggeredObject.tag}");

            // �±װ� Null���� üũ
            if (string.IsNullOrEmpty(currentInteractable.tag) || string.IsNullOrEmpty(triggeredObject.tag))
            {
                Debug.LogWarning("[OnClickButton] �±װ� Null�̰ų� ��� �ֽ��ϴ�.");
                return;
            }


            if (triggeredObject.CompareTag("CorrectNumber"))
            {
                //if (stageclear != null)
                //{
                photonView.RPC("SetStage3Clear", RpcTarget.All);
                Debug.Log("���� ������Ʈ�� ��ȣ�ۿ�: �������� Ŭ���� ó��");
                //}
            }
        }
        else
        {
            Debug.LogWarning("[OnClickButton] currentInteractable �Ǵ� triggeredObject�� null�Դϴ�.");
            if (button != null && button.targetGraphic != null)
            {
                StartCoroutine(ChangeButtonColorTemporarily(Color.red, 3f));
                Debug.Log("���� ������Ʈ�� ��ȣ�ۿ�: ��ư ������");
            }
        }
    }



    [PunRPC]
    void SetStage3Clear()
    {
        Debug.Log("SetStage3Clear ȣ���");
        if (stageclear != null)
        {
            stageclear.stage3clear = true;
            Debug.Log("stage3clear ���� �Ϸ�");
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
