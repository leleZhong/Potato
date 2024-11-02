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

        // 버튼의 OnClick 이벤트에 OnClickButton을 할당
        SetupButton();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(true); // 상호작용 UI 활성화

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
            interactionUI.SetActive(false); // 상호작용 UI 비활성화

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

    // 캐릭터 생성 후 버튼 이벤트를 설정하는 메서드
    public void SetupButton()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners(); // 기존 리스너 제거
            button.onClick.AddListener(OnClickButton); // 새 리스너 추가
        }
    }
}
