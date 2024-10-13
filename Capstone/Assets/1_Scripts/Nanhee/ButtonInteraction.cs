using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject interactionUI; // ��ȣ�ۿ� UI
    private GameObject currentInteractable; // ���� ��ȣ�ۿ� ������ ������Ʈ
    public Animator doorAnimator; // �� �ִϸ����� (�̸� �Ҵ�)
    public Button button;
    private static readonly string DoorOpen = "DoorOpen";
    private StageClear stageclear;

    void Start()
    {
        interactionUI.SetActive(false); // ó���� UI�� ��Ȱ��ȭ
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(true); // ��ȣ�ۿ� UI Ȱ��ȭ

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
            interactionUI.SetActive(false); // ��ȣ�ۿ� UI ��Ȱ��ȭ

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
        
        // currentInteractable�� CorrectNumber �±׸� ������ �ִ� ��쿡�� �� ���� �ִϸ��̼� ����
        if (currentInteractable != null && currentInteractable.CompareTag("CorrectNumber"))
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetBool(DoorOpen, true);
                //stageclear.SetBool(true);
                if(stageclear != null)
                {
                    stageclear.stage3clear = true;
                }
            }
        }
        else
        {
            // �߸��� ��ȣ�ۿ��� �� ��ư�� ���������� ����
            if (button.targetGraphic != null)
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
