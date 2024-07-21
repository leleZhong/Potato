using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Transform[] InterActionTransform;
    public GameObject[] blockPrefabs; // 블럭 프리팹 배열
    public GameObject interactionUI; // 상호작용 UI
    public Animator DoorOpen; // Door의 Animator
    public Animator AnswerDoorOpen; // AnswerDoor의 Animator

    private GameObject currentInteractable; // 현재 상호작용 가능한 오브젝트

    void Start()
    {
        Invoke("MakeQuestion", 0.1f);
        interactionUI.SetActive(false); // 처음에 UI를 비활성화
    }

    void Update()
    {
        // E 키를 눌렀을 때 상호작용
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            OnClickButton(currentInteractable);
        }
    }

    void MakeQuestion()
    {
        List<int> InteractionNumbers = new List<int>() { 0, 1, 2, 3 }; // 생성 후보 번호들
        int answerIndex = Random.Range(0, blockPrefabs.Length); // CorrectNumber 프리팹 인덱스 선택
        GameObject duplicatedBlock = blockPrefabs[answerIndex];
        duplicatedBlock.tag = "CorrectNumber";

        if (duplicatedBlock != null)
        {
            int index1 = Random.Range(0, InterActionTransform.Length);
            GameObject correctAnswerBlock = Instantiate(duplicatedBlock, InterActionTransform[index1].position, Quaternion.identity); // 객체 생성

            // correctAnswerBlock의 프리팹 인덱스를 찾아서 InteractionNumbers에서 제거
            int correctAnswerIndex = FindPrefabIndex(correctAnswerBlock);
            if (correctAnswerIndex != -1)
            {
                InteractionNumbers.Remove(correctAnswerIndex);
            }

            correctAnswerBlock.transform.parent = InterActionTransform[index1];
            correctAnswerBlock.transform.localPosition = Vector3.zero;

            foreach (Transform cubeTransform in InterActionTransform)
            {
                if (cubeTransform == InterActionTransform[index1]) continue; // 이미 중복 블록이 할당된 위치는 건너뛰기
                Debug.Log("Skipping duplicated block allocation at index: " + index1);

                int randomIndex = Random.Range(0, InteractionNumbers.Count);
                int selectedNumber = InteractionNumbers[randomIndex];

                GameObject block = Instantiate(blockPrefabs[selectedNumber], cubeTransform.position, Quaternion.identity); // 선택된 블럭프리펩을 위치에 할당
                block.transform.parent = cubeTransform;

                InteractionNumbers.RemoveAt(randomIndex); // 할당된 숫자를 리스트에서 지우기
            }
        }
        else
        {
            Debug.LogError("Correct answer prefab not found.");
        }
    }

    int FindPrefabIndex(GameObject block)
    {
        for (int i = 0; i < blockPrefabs.Length; i++)
        {
            if (blockPrefabs[i].name == block.name.Replace("(Clone)", "").Trim())
            {
                return i;
            }
        }
        return -1;
    }

    // Trigger event handlers for interaction UI
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

    public void OnClickButton(GameObject interactable)
    {
        if (interactable.CompareTag("CorrectNumber"))
        {
            // CorrectNumber 태그를 가진 오브젝트와 상호작용했을 때 DoorOpen 애니메이션 재생
            if (DoorOpen != null)
            {
                DoorOpen.SetTrigger("DoorOpen");
            }
            if (AnswerDoorOpen != null)
            {
                AnswerDoorOpen.SetTrigger("DoorOpen");
            }
        }
        else
        {
            // 다른 상호작용 처리 (필요한 경우 추가)
        }
    }
   
}
