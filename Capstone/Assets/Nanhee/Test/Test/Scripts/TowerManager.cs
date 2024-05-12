using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject[] BlockGroup; // ��� �׷� �������� ���� �迭

    private List<int> usedIndices = new List<int>(); // �̹� ���� �ε����� �����ϱ� ���� ����Ʈ

    void Start()
    {
        AssignBlockGroups();
    }

    void AssignBlockGroups()
    {
        GameObject tower = GameObject.Find("Tower"); // Tower ���� ������Ʈ�� ã��
        if (tower != null)
        {
            // �� BlockPos�� �ߺ����� �ʴ� ��� �׷� �Ҵ�
            for (int i = 1; i <= 3; i++)
            {
                GameObject blockPos = transform.Find("BlockPos" + i).gameObject;
                GameObject randomBlockGroup = GetRandomBlockGroup();
                if (randomBlockGroup != null)
                {
                    Instantiate(randomBlockGroup, blockPos.transform.position, Quaternion.identity, blockPos.transform);
                }
            }
        }
    }

    GameObject GetRandomBlockGroup()
    {
        // ��� ������ �ε��� ��� �ʱ�ȭ
        if (usedIndices.Count == BlockGroup.Length)
        {
            usedIndices.Clear();
        }

        // ��� ������ �ε��� ã��
        int randomIndex = Random.Range(0, BlockGroup.Length);
        while (usedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, BlockGroup.Length);
        }

        // ���õ� �ε��� ������� ǥ��
        usedIndices.Add(randomIndex);

        // ���õ� ��� �׷� ��ȯ
        return BlockGroup[randomIndex];
    }
}