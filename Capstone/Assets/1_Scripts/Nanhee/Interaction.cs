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
        //Debug.Log($"[�����] ���� ��� ����: {duplicatedBlock.name}");

        // ���� ��� ����
        int index1 = Random.Range(0, InterActionTransform.Length);
        GameObject correctAnswerBlock = Instantiate(duplicatedBlock, InterActionTransform[index1].position, Quaternion.identity);
        correctAnswerBlock.transform.parent = InterActionTransform[index1];
        correctAnswerBlock.transform.localPosition = Vector3.zero;

        // Rigidbody �߰�
        Rigidbody rb = correctAnswerBlock.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = correctAnswerBlock.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }

        //Debug.Log($"[�����] ���� ����� ��ġ�� Transform �ε���: {index1}");

        // InterActionTransform���� ������ ���� ��ġ�� ������ Interaction ��� ����
        List<int> remainingIndices = new List<int>();
        for (int i = 0; i < InterActionTransform.Length; i++)
        {
            if (i != index1)
                remainingIndices.Add(i);
        }

        // BlockManager���� ������ duplicatedBlockIndex�� ����� ���� ����� ����Ʈ���� ����
        List<int> availableNumbers = new List<int>() { 0, 1, 2, 3, 4 };
        availableNumbers.Remove(BlockManager.duplicatedBlockIndex);
        //Debug.Log($"[�����] ���� ��� �ε��� {BlockManager.duplicatedBlockIndex} ���� �� ��� ������ ��ȣ��: {string.Join(", ", availableNumbers)}");

        // ���� ��ġ�� �� �������� �ϳ����� �Ҵ�
        for (int i = 0; i < remainingIndices.Count && i < availableNumbers.Count; i++)
        {
            int selectedNumber = availableNumbers[i];
            //Debug.Log($"[�����] ���� ��ġ�� �Ҵ��� ��� ������ �ε���: {selectedNumber}, ��ġ �ε���: {remainingIndices[i]}");

            GameObject block = Instantiate(blockPrefabs[selectedNumber], InterActionTransform[remainingIndices[i]].position, Quaternion.identity);
            block.transform.parent = InterActionTransform[remainingIndices[i]];
            block.tag = "Interaction";

            // Rigidbody �߰�
            rb = block.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = block.AddComponent<Rigidbody>();
                rb.isKinematic = true;
            }
        }
    }
}
