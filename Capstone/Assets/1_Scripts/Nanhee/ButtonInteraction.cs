using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour
{
    public PhotonView photonView;

    public GameObject interactionUI; // 상호작용 UI
    private GameObject currentInteractable; // 현재 상호작용 가능한 오브젝트
    public Button button;
    public StageClear stageclear;

    void Awake()
    {
        // InteractionUI 오브젝트 찾기
        interactionUI = GameObject.Find("InteractionUI");

        if (interactionUI != null)
        {
            // InteractionUI 내부의 "Panel/InteractionButton" 오브젝트 찾기
            Transform buttonTransform = interactionUI.transform.Find("Panel/InteractionButton");

            if (buttonTransform != null)
            {
                button = buttonTransform.GetComponent<Button>();

                if (button != null)
                {
                    Debug.Log("Button 컴포넌트가 자동으로 할당되었습니다: " + button.name);
                }
                else
                {
                    Debug.LogWarning("InteractionButton 오브젝트에 Button 컴포넌트가 없습니다.");
                }
            }
            else
            {
                Debug.LogWarning("Panel/InteractionButton 오브젝트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogWarning("InteractionUI 오브젝트를 찾을 수 없습니다.");
        }
    }

    void Start()
    {
        stageclear = FindObjectOfType<StageClear>();
        if (interactionUI != null)
        {
            interactionUI.SetActive(false); // 처음에 UI를 비활성화
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            if (interactionUI != null)
            {
                interactionUI.SetActive(true); // 상호작용 UI 활성화
            }

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
            if (interactionUI != null)
            {
                interactionUI.SetActive(false); // 상호작용 UI 비활성화
            }

            // 현재 상호작용 오브젝트가 나가는 오브젝트와 동일한 경우 해제
            if (currentInteractable == other.gameObject)
            {
                currentInteractable = null;
            }
        }
    }

    public void OnClickButton()
    {
        // currentInteractable이 CorrectNumber 태그를 가지고 있는 경우에만 stage3clear 설정
        if (currentInteractable != null && currentInteractable.CompareTag("CorrectNumber"))
        {
            if (stageclear != null)
            {
                // RPC를 통해 모든 클라이언트에 stage3clear 상태를 동기화
                photonView.RPC("SetStage3Clear", RpcTarget.All);
            }
        }
        else
        {
            // 잘못된 상호작용일 때 버튼을 빨간색으로 변경
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
