using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryTower : MonoBehaviour
{
    public GameObject[] blockPrefabs; // 블럭 프리팹 배열
    public Transform[] group2Transforms; // 그룹 2 위치 배열
    private int duplicatedNumber; // 중복된 번호를 저장할 변수

    // 중복된 번호를 받아옴
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
        List<int> availableNumbers = new List<int>() { 1, 2, 3, 4, 5, 6 }; // 생성 후보 번호들

        List<int> group2AssignedNumbers = new List<int>(); // 그룹 2에 할당된 번호들

        // 그룹 2에 랜덤하게 그림 할당
        foreach (Transform cubeTransform in group2Transforms)
        {
            int selectedNumber;

            // 중복된 번호가 있을 경우 중복된 번호를 할당하고 그렇지 않으면 랜덤한 번호를 할당
            if (duplicatedNumber != 0)
            {
                selectedNumber = duplicatedNumber;
            }
            else
            {
                int randomIndex = Random.Range(0, availableNumbers.Count);
                selectedNumber = availableNumbers[randomIndex];
            }

            // 블럭 생성 및 위치 설정
            GameObject block = Instantiate(blockPrefabs[selectedNumber - 1], cubeTransform.position, Quaternion.identity);
            block.transform.parent = cubeTransform;

            // Debug.Log(cubeTransform.name + ": " + selectedNumber); // 번호 확인용 디버그 메시지

            // 중복된 번호가 할당된 경우에만 할당된 번호 추가
            if (duplicatedNumber != 0)
            {
                // 할당된 번호 추가
                group2AssignedNumbers.Add(selectedNumber);
            }
        }
    }
}
