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
        if (BlockManager.duplicatedBlockPrefab == null)
        {
            Debug.LogError("BlockManager에서 duplicatedBlockPrefab이 설정되지 않았습니다.");
            return;
        }

        // 모든 프리팹에 기본적으로 "Interaction" 태그를 설정
        foreach (GameObject blockPrefab in blockPrefabs)
        {
            blockPrefab.tag = "Interaction";
        }

        // BlockManager에서 설정한 duplicatedBlockPrefab과 동일한 프리팹을 CorrectNumber로 설정
        GameObject duplicatedBlock = BlockManager.duplicatedBlockPrefab;
        duplicatedBlock.tag = "CorrectNumber";

        // 정답 블록 생성
        int index1 = Random.Range(0, InterActionTransform.Length);
        GameObject correctAnswerBlock = Instantiate(duplicatedBlock, InterActionTransform[index1].position, Quaternion.identity);

        // Rigidbody 추가
        Rigidbody rb = correctAnswerBlock.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = correctAnswerBlock.AddComponent<Rigidbody>();
            rb.isKinematic = true; // 정적 오브젝트로 설정
        }

        correctAnswerBlock.transform.parent = InterActionTransform[index1];
        correctAnswerBlock.transform.localPosition = Vector3.zero;

        // InterActionTransform에서 사용되지 않은 위치에 나머지 Interaction 블록 생성
        List<int> remainingIndices = new List<int>();
        for (int i = 0; i < InterActionTransform.Length; i++)
        {
            if (i != index1)
                remainingIndices.Add(i);
        }

        // BlockManager에서 사용된 duplicatedBlockPrefab을 제외한 나머지 블록 프리팹을 랜덤하게 할당
        List<int> availableNumbers = new List<int>() { 0, 1, 2, 3, 4 };
        int correctNumberIndex = System.Array.IndexOf(blockPrefabs, duplicatedBlock);
        availableNumbers.Remove(correctNumberIndex);

        // 남은 위치에 각 프리팹을 하나씩만 할당
        foreach (int index in remainingIndices)
        {
            if (availableNumbers.Count == 0) break;

            int randomIndex = Random.Range(0, availableNumbers.Count);
            int selectedNumber = availableNumbers[randomIndex];

            GameObject block = Instantiate(blockPrefabs[selectedNumber], InterActionTransform[index].position, Quaternion.identity);
            block.transform.parent = InterActionTransform[index];
            block.tag = "Interaction";

            // Rigidbody 추가
            rb = block.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = block.AddComponent<Rigidbody>();
                rb.isKinematic = true;
            }

            // 선택된 프리팹은 리스트에서 제거하여 중복되지 않도록 함
            availableNumbers.RemoveAt(randomIndex);
        }
    }
}
