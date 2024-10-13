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
        List<int> InteractionNumbers = new List<int>() { 0, 1, 2, 3, 4 }; // 생성 후보 번호들
        int answerIndex = Random.Range(0, blockPrefabs.Length); // CorrectNumber 프리팹 인덱스 선택
        GameObject duplicatedBlock = blockPrefabs[answerIndex];
        
        for(int i = 0; i < 5; i++)
        {
            blockPrefabs[i].tag = "Interaction";
        }

        duplicatedBlock.tag = "CorrectNumber";

        if (duplicatedBlock != null)
        {
            int index1 = Random.Range(0, InterActionTransform.Length);
            GameObject correctAnswerBlock = Instantiate(duplicatedBlock, InterActionTransform[index1].position, Quaternion.identity); // 객체 생성

            // Rigidbody 추가
            Rigidbody rb = correctAnswerBlock.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = correctAnswerBlock.AddComponent<Rigidbody>();
                rb.isKinematic = true; // 정적 오브젝트로 설정
            }

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

                int randomIndex = Random.Range(0, InteractionNumbers.Count);
                int selectedNumber = InteractionNumbers[randomIndex];

                GameObject block = Instantiate(blockPrefabs[selectedNumber], cubeTransform.position, Quaternion.identity); // 선택된 블럭프리펩을 위치에 할당
                block.transform.parent = cubeTransform;

                // Rigidbody 추가
                rb = block.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = block.AddComponent<Rigidbody>();
                    rb.isKinematic = true; // 정적 오브젝트로 설정
                }

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
}
