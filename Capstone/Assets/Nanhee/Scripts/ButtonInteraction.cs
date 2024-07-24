using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject interactionUI; // ��ȣ�ۿ� UI
    private GameObject currentInteractable; // ���� ��ȣ�ۿ� ������ ������Ʈ

    void Start()
    {
        interactionUI.SetActive(false); // ó���� UI�� ��Ȱ��ȭ
    }

    void Update()
    {
        
            InteractWithObject(currentInteractable);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(true); // ��ȣ�ۿ� UI Ȱ��ȭ
            currentInteractable = other.gameObject; // ���� ��ȣ�ۿ� ������ ������Ʈ ����
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(false); // ��ȣ�ۿ� UI ��Ȱ��ȭ
            currentInteractable = null; // ���� ��ȣ�ۿ� ������ ������Ʈ ����
        }
    }

    public void InteractWithObject(GameObject interactable)
    {
        if (interactable.CompareTag("CorrectNumber"))
        {
            // CorrectNumber �±׸� ���� ������Ʈ�� ��ȣ�ۿ����� �� DoorOpen �ִϸ��̼� ���
            Animator doorAnimator = interactable.GetComponent<Animator>();
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("DoorOpen");
            }
        }
        else
        {
            // �ٸ� ��ȣ�ۿ� ó�� (�ʿ��� ��� �߰�)
        }
    }
}
