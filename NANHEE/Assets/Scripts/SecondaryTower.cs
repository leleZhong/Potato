using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryTower : MonoBehaviour
{
    public GameObject[] blockPrefabs; // �� ������ �迭
    public Transform[] group2Transforms; // �׷� 2 ��ġ �迭
    private int duplicatedNumber; // �ߺ��� ��ȣ�� ������ ����

    // �ߺ��� ��ȣ�� �޾ƿ�
    public void ReceiveDuplicateNumber(int number)
    {
        duplicatedNumber = number;
    }

    void Start()
    {
        InitializeBlocks();
    }

    void InitializeBlocks()
    {
        List<int> availableNumbers = new List<int>() { 1, 2, 3, 4, 5, 6 }; // ���� �ĺ� ��ȣ��

        List<int> group2AssignedNumbers = new List<int>(); // �׷� 2�� �Ҵ�� ��ȣ��

        // �׷� 2�� �����ϰ� �׸� �Ҵ�
        foreach (Transform cubeTransform in group2Transforms)
        {
            int selectedNumber;

            // �ߺ��� ��ȣ�� ���� ��� �ߺ��� ��ȣ�� �Ҵ��ϰ� �׷��� ������ ������ ��ȣ�� �Ҵ�
            if (duplicatedNumber != 0)
            {
                selectedNumber = duplicatedNumber;
            }
            else
            {
                int randomIndex = Random.Range(0, availableNumbers.Count);
                selectedNumber = availableNumbers[randomIndex];
            }

            // �� ���� �� ��ġ ����
            GameObject block = Instantiate(blockPrefabs[selectedNumber - 1], cubeTransform.position, Quaternion.identity);
            block.transform.parent = cubeTransform;

            // Debug.Log(cubeTransform.name + ": " + selectedNumber); // ��ȣ Ȯ�ο� ����� �޽���

            // �ߺ��� ��ȣ�� �Ҵ�� ��쿡�� �Ҵ�� ��ȣ �߰�
            if (duplicatedNumber != 0)
            {
                // �Ҵ�� ��ȣ �߰�
                group2AssignedNumbers.Add(selectedNumber);
            }
        }
    }
}
