using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject interactionUI; // 상호작용 UI
    private GameObject currentInteractable; // 현재 상호작용 가능한 오브젝트
    public Animator doorAnimator; // 문 애니메이터 (미리 할당)
    public Button button;
    private static readonly string DoorOpen = "DoorOpen";
    private StageClear stageclear;

    void Start()
    {
        interactionUI.SetActive(false); // 처음에 UI를 비활성화
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(true); // 상호작용 UI 활성화

            // 만약 해당 오브젝트가 CorrectNumber 태그를 가지고 있다면 currentInteractable로 설정
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
            interactionUI.SetActive(false); // 상호작용 UI 비활성화

            // 현재 상호작용 오브젝트가 나가는 오브젝트와 동일한 경우 해제
            if (currentInteractable == other.gameObject)
            {
                currentInteractable = null;
            }
        }
    }

    public void OnClickButton()
    {
        stageclear = GameObject.Find("StageClear").GetComponent<StageClear>();
        
        // currentInteractable이 CorrectNumber 태그를 가지고 있는 경우에만 문 열기 애니메이션 실행
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
            // 잘못된 상호작용일 때 버튼을 빨간색으로 변경
            if (button.targetGraphic != null)
            {
                StartCoroutine(ChangeButtonColorTemporarily(Color.red, 3f));
            }
        }
    }

    private IEnumerator ChangeButtonColorTemporarily(Color color, float duration)
    {
        // 기존 색상을 저장
        Color originalColor = button.targetGraphic.color;
        // 색상을 변경
        button.targetGraphic.color = color;
        // duration만큼 대기
        yield return new WaitForSeconds(duration);
        // 원래 색상으로 복원
        button.targetGraphic.color = originalColor;
    }

}
