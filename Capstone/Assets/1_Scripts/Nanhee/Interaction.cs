using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Transform[] InterActionTransform;
    public GameObject[] blockPrefabs; // �� ������ �迭

    void Start()
    {
        Invoke("MakeQuestion", 0.1f);
    }

    void MakeQuestion()
    {
        List<int> InteractionNumbers = new List<int>() { 0, 1, 2, 3, 4 }; // ���� �ĺ� ��ȣ��
        int answerIndex = Random.Range(0, blockPrefabs.Length); // CorrectNumber ������ �ε��� ����
        GameObject duplicatedBlock = blockPrefabs[answerIndex];
        
        for(int i = 0; i < 5; i++)
        {
            blockPrefabs[i].tag = "Interaction";
        }

        duplicatedBlock.tag = "CorrectNumber";

        if (duplicatedBlock != null)
        {
            int index1 = Random.Range(0, InterActionTransform.Length);
            GameObject correctAnswerBlock = Instantiate(duplicatedBlock, InterActionTransform[index1].position, Quaternion.identity); // ��ü ����

            // Rigidbody �߰�
            Rigidbody rb = correctAnswerBlock.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = correctAnswerBlock.AddComponent<Rigidbody>();
                rb.isKinematic = true; // ���� ������Ʈ�� ����
            }

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

                int randomIndex = Random.Range(0, InteractionNumbers.Count);
                int selectedNumber = InteractionNumbers[randomIndex];

                GameObject block = Instantiate(blockPrefabs[selectedNumber], cubeTransform.position, Quaternion.identity); // ���õ� ���������� ��ġ�� �Ҵ�
                block.transform.parent = cubeTransform;

                // Rigidbody �߰�
                rb = block.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = block.AddComponent<Rigidbody>();
                    rb.isKinematic = true; // ���� ������Ʈ�� ����
                }

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
}
