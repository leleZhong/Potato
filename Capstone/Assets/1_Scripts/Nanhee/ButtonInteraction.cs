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
    private GameObject triggeredObject;

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
        // BlockManager 인스턴스 찾기
        BlockManager blockManager = FindObjectOfType<BlockManager>();
        if (blockManager != null && blockManager.correctBlock != null)
        {
            currentInteractable = blockManager.correctBlock;
            Debug.Log($"[ButtonInteraction] CorrectNumber 블록이 currentInteractable로 설정됨: {currentInteractable.name}");
        }

        // 기존 초기화 로직
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
        Debug.Log($"[TriggerEnter] other.name: {other.gameObject.name}, 태그: {other.tag}");

        // 태그가 null이거나 비어 있는 경우 필터링
        if (string.IsNullOrEmpty(other.tag))
        {
            Debug.LogWarning($"[TriggerEnter] 태그가 Null이거나 비어 있는 오브젝트: {other.gameObject.name}");
            return; // 처리 중단
        }

        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(true);

            if (other.CompareTag("CorrectNumber"))
            {
                triggeredObject = other.gameObject;
                Debug.Log($"[CorrectNumber] triggeredObject 설정: {triggeredObject.name}");
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"[TriggerExit] {other.gameObject.name}가 Trigger에서 벗어남");

        if (string.IsNullOrEmpty(other.tag))
        {
            Debug.LogWarning($"[TriggerExit] 태그가 Null이거나 비어 있는 오브젝트: {other.gameObject.name}");
            return;
        }

        if (other.CompareTag("Interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(false); // 상호작용 UI 비활성화

            if (triggeredObject == other.gameObject)
            {
                Debug.Log("[TriggerExit] triggeredObject 초기화");
                triggeredObject = null;
            }
        }
    }




    public void OnClickButton()
    {
        if (currentInteractable != null && triggeredObject != null)
        {
            Debug.Log($"[OnClickButton] currentInteractable: {currentInteractable.name}, 태그: {currentInteractable.tag}");
            Debug.Log($"[OnClickButton] triggeredObject: {triggeredObject.name}, 태그: {triggeredObject.tag}");

            // 태그가 Null인지 체크
            if (string.IsNullOrEmpty(currentInteractable.tag) || string.IsNullOrEmpty(triggeredObject.tag))
            {
                Debug.LogWarning("[OnClickButton] 태그가 Null이거나 비어 있습니다.");
                return;
            }


            if (triggeredObject.CompareTag("CorrectNumber"))
            {
                //if (stageclear != null)
                //{
                photonView.RPC("SetStage3Clear", RpcTarget.All);
                Debug.Log("정답 오브젝트와 상호작용: 스테이지 클리어 처리");
                //}
            }
        }
        else
        {
            Debug.LogWarning("[OnClickButton] currentInteractable 또는 triggeredObject가 null입니다.");
            if (button != null && button.targetGraphic != null)
            {
                StartCoroutine(ChangeButtonColorTemporarily(Color.red, 3f));
                Debug.Log("오답 오브젝트와 상호작용: 버튼 빨간색");
            }
        }
    }



    [PunRPC]
    void SetStage3Clear()
    {
        Debug.Log("SetStage3Clear 호출됨");
        if (stageclear != null)
        {
            stageclear.stage3clear = true;
            Debug.Log("stage3clear 설정 완료");
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
