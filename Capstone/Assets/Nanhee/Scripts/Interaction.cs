using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Transform[] InterActionTransform;
    public GameObject[] blockPrefabs; // �� ������ �迭
    public GameObject interactionUI; // ��ȣ�ۿ� UI
    public Animator DoorOpen; // Door�� Animator
    public Animator AnswerDoorOpen; // AnswerDoor�� Animator

    private GameObject currentInteractable; // ���� ��ȣ�ۿ� ������ ������Ʈ

    void Start()
    {
        Invoke("MakeQuestion", 0.1f);
        interactionUI.SetActive(false); // ó���� UI�� ��Ȱ��ȭ
    }

    void Update()
    {
        // E Ű�� ������ �� ��ȣ�ۿ�
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            OnClickButton(currentInteractable);
        }
    }

    void MakeQuestion()
    {
        List<int> InteractionNumbers = new List<int>() { 0, 1, 2, 3 }; // ���� �ĺ� ��ȣ��
        int answerIndex = Random.Range(0, blockPrefabs.Length); // CorrectNumber ������ �ε��� ����
        GameObject duplicatedBlock = blockPrefabs[answerIndex];
        duplicatedBlock.tag = "CorrectNumber";

        if (duplicatedBlock != null)
        {
            int index1 = Random.Range(0, InterActionTransform.Length);
            GameObject correctAnswerBlock = Instantiate(duplicatedBlock, InterActionTransform[index1].position, Quaternion.identity); // ��ü ����

            // correctAnswerBlock�� ������ �ε����� ã�Ƽ� InteractionNumbers���� ����
            int correctAnswerIndex = FindPrefabIndex(correctAnswerBlock);
            if (correctAnswerIndex != -1)
            {
                InteractionNumbers.Remove(correctAnswerIndex);
            }

            correctAnswerBlock.transform.parent = InterActionTransform[index1];
            correctAnswerBlock.transform.localPosition = Vector3.zero;

            foreach (Transform cubeTransform in InterActionTransform)
            {
                if (cubeTransform == InterActionTransform[index1]) continue; // �̹� �ߺ� ����� �Ҵ�� ��ġ�� �ǳʶٱ�
                Debug.Log("Skipping duplicated block allocation at index: " + index1);

                int randomIndex = Random.Range(0, InteractionNumbers.Count);
                int selectedNumber = InteractionNumbers[randomIndex];

                GameObject block = Instantiate(blockPrefabs[selectedNumber], cubeTransform.position, Quaternion.identity); // ���õ� ���������� ��ġ�� �Ҵ�
                block.transform.parent = cubeTransform;

                InteractionNumbers.RemoveAt(randomIndex); // �Ҵ�� ���ڸ� ����Ʈ���� �����
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

    public void OnClickButton(GameObject interactable)
    {
        if (interactable.CompareTag("CorrectNumber"))
        {
            // CorrectNumber �±׸� ���� ������Ʈ�� ��ȣ�ۿ����� �� DoorOpen �ִϸ��̼� ���
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
            // �ٸ� ��ȣ�ۿ� ó�� (�ʿ��� ��� �߰�)
        }
    }
   
}
