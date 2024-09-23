using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject interactionUI; // 상호작용 UI
    private GameObject currentInteractable; // 현재 상호작용 가능한 오브젝트
    public Animator animator1;
    public Animator animator2;

    void Start()
    {
        interactionUI.SetActive(false); // 처음에 UI를 비활성화
        animator1 = GetComponent<Animator>();
        animator2 = GetComponent<Animator>();
    }

    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(true); // 상호작용 UI 활성화
            currentInteractable = other.gameObject; // 현재 상호작용 가능한 오브젝트 설정
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(false); // 상호작용 UI 비활성화
            currentInteractable = null; // 현재 상호작용 가능한 오브젝트 해제
        }
    }

    public void InteractWithObject()
    {
        if (currentInteractable != null && currentInteractable.CompareTag("CorrectNumber"))
        {
            // CorrectNumber 태그를 가진 오브젝트와 상호작용했을 때 DoorOpen 애니메이션 재생
            Animator doorAnimator = currentInteractable.GetComponent<Animator>();
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("DoorOpen");
            }
        }
        else
        {
            // 다른 상호작용 처리 (필요한 경우 추가)
        }
    }
}
