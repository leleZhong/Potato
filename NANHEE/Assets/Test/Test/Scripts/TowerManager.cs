using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject[] BlockGroup; // 블록 그룹 프리팹을 담을 배열

    private List<int> usedIndices = new List<int>(); // 이미 사용된 인덱스를 추적하기 위한 리스트

    void Start()
    {
        AssignBlockGroups();
    }

    void AssignBlockGroups()
    {
        GameObject tower = GameObject.Find("Tower"); // Tower 게임 오브젝트를 찾음
        if (tower != null)
        {
            // 각 BlockPos에 중복되지 않는 블록 그룹 할당
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
        // 사용 가능한 인덱스 목록 초기화
        if (usedIndices.Count == BlockGroup.Length)
        {
            usedIndices.Clear();
        }

        // 사용 가능한 인덱스 찾기
        int randomIndex = Random.Range(0, BlockGroup.Length);
        while (usedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, BlockGroup.Length);
        }

        // 선택된 인덱스 사용으로 표시
        usedIndices.Add(randomIndex);

        // 선택된 블록 그룹 반환
        return BlockGroup[randomIndex];
    }
}