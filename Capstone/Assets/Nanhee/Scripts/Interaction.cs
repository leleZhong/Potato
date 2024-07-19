using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Transform[] InterActionTransform;
    public GameObject[] blockPrefabs; // 블럭 프리팹 배열

    void Start()
    {
        Invoke("MakeQuestion", 0.1f);
    }

    void MakeQuestion()
    {
        List<int> InteractionNumbers = new List<int>() { 0, 1, 2, 3 }; // 생성 후보 번호들
        GameObject duplicatedBlock = GameObject.FindWithTag("CorrectNumber");

        if (duplicatedBlock != null)
        {
            int index1 = Random.Range(0, InterActionTransform.Length);
            GameObject correctAnswerBlock = Instantiate(duplicatedBlock, InterActionTransform[index1].position, Quaternion.identity); // 객체 생성

            // 정답 블록 위치와 동일한 인덱스의 InteractionNumbers 요소 제거
            InteractionNumbers.RemoveAt(index1);

            correctAnswerBlock.transform.parent = InterActionTransform[index1];
            correctAnswerBlock.transform.localPosition = Vector3.zero;

            foreach (Transform cubeTransform in InterActionTransform)
            {
                if (cubeTransform == InterActionTransform[index1]) continue; // 이미 중복 블록이 할당된 위치는 건너뛰기
                Debug.Log("Skipping duplicated block allocation at index: " + index1);

                int randomIndex = Random.Range(0, InteractionNumbers.Count);
                int selectedNumber = InteractionNumbers[randomIndex];

                GameObject block = Instantiate(blockPrefabs[selectedNumber], cubeTransform.position, Quaternion.identity); //선택된 블럭프리펩을 위치에 할당
                block.transform.parent = cubeTransform;

                InteractionNumbers.RemoveAt(randomIndex); //할당된 숫자를 리스트에서 지우기
            }
        }
        else
        {
            Debug.LogError("Duplicated block with tag 'DuplicatedBlock' not found.");
        }
    }
}
