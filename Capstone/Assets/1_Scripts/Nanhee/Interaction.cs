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
        if (BlockManager.duplicatedBlockPrefab == null)
        {
            Debug.LogError("BlockManager���� duplicatedBlockPrefab�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ��� �����տ� �⺻������ "Interaction" �±׸� ����
        foreach (GameObject blockPrefab in blockPrefabs)
        {
            blockPrefab.tag = "Interaction";
        }

        // BlockManager���� ������ duplicatedBlockPrefab�� ������ �������� CorrectNumber�� ����
        GameObject duplicatedBlock = BlockManager.duplicatedBlockPrefab;
        duplicatedBlock.tag = "CorrectNumber";

        // ���� ��� ����
        int index1 = Random.Range(0, InterActionTransform.Length);
        GameObject correctAnswerBlock = Instantiate(duplicatedBlock, InterActionTransform[index1].position, Quaternion.identity);

        // Rigidbody �߰�
        Rigidbody rb = correctAnswerBlock.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = correctAnswerBlock.AddComponent<Rigidbody>();
            rb.isKinematic = true; // ���� ������Ʈ�� ����
        }

        correctAnswerBlock.transform.parent = InterActionTransform[index1];
        correctAnswerBlock.transform.localPosition = Vector3.zero;

        // InterActionTransform���� ������ ���� ��ġ�� ������ Interaction ��� ����
        List<int> remainingIndices = new List<int>();
        for (int i = 0; i < InterActionTransform.Length; i++)
        {
            if (i != index1)
                remainingIndices.Add(i);
        }

        // BlockManager���� ���� duplicatedBlockPrefab�� ������ ������ ��� �������� �����ϰ� �Ҵ�
        List<int> availableNumbers = new List<int>() { 0, 1, 2, 3, 4 };
        int correctNumberIndex = System.Array.IndexOf(blockPrefabs, duplicatedBlock);
        availableNumbers.Remove(correctNumberIndex);

        // ���� ��ġ�� �� �������� �ϳ����� �Ҵ�
        foreach (int index in remainingIndices)
        {
            if (availableNumbers.Count == 0) break;

            int randomIndex = Random.Range(0, availableNumbers.Count);
            int selectedNumber = availableNumbers[randomIndex];

            GameObject block = Instantiate(blockPrefabs[selectedNumber], InterActionTransform[index].position, Quaternion.identity);
            block.transform.parent = InterActionTransform[index];
            block.tag = "Interaction";

            // Rigidbody �߰�
            rb = block.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = block.AddComponent<Rigidbody>();
                rb.isKinematic = true;
            }

            // ���õ� �������� ����Ʈ���� �����Ͽ� �ߺ����� �ʵ��� ��
            availableNumbers.RemoveAt(randomIndex);
        }
    }
}
