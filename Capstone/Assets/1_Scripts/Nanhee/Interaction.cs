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
        //Debug.Log($"[디버그] 정답 블록 설정: {duplicatedBlock.name}");

        // 정답 블록 생성
        int index1 = Random.Range(0, InterActionTransform.Length);
        GameObject correctAnswerBlock = Instantiate(duplicatedBlock, InterActionTransform[index1].position, Quaternion.identity);
        correctAnswerBlock.transform.parent = InterActionTransform[index1];
        correctAnswerBlock.transform.localPosition = Vector3.zero;

        // Rigidbody 추가
        Rigidbody rb = correctAnswerBlock.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = correctAnswerBlock.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }

        //Debug.Log($"[디버그] 정답 블록이 위치한 Transform 인덱스: {index1}");

        // InterActionTransform에서 사용되지 않은 위치에 나머지 Interaction 블록 생성
        List<int> remainingIndices = new List<int>();
        for (int i = 0; i < InterActionTransform.Length; i++)
        {
            if (i != index1)
                remainingIndices.Add(i);
        }

        // BlockManager에서 설정한 duplicatedBlockIndex를 사용해 정답 블록을 리스트에서 제거
        List<int> availableNumbers = new List<int>() { 0, 1, 2, 3, 4 };
        availableNumbers.Remove(BlockManager.duplicatedBlockIndex);
        //Debug.Log($"[디버그] 정답 블록 인덱스 {BlockManager.duplicatedBlockIndex} 제거 후 사용 가능한 번호들: {string.Join(", ", availableNumbers)}");

        // 남은 위치에 각 프리팹을 하나씩만 할당
        for (int i = 0; i < remainingIndices.Count && i < availableNumbers.Count; i++)
        {
            int selectedNumber = availableNumbers[i];
            //Debug.Log($"[디버그] 남은 위치에 할당할 블록 프리팹 인덱스: {selectedNumber}, 위치 인덱스: {remainingIndices[i]}");

            GameObject block = Instantiate(blockPrefabs[selectedNumber], InterActionTransform[remainingIndices[i]].position, Quaternion.identity);
            block.transform.parent = InterActionTransform[remainingIndices[i]];
            block.tag = "Interaction";

            // Rigidbody 추가
            rb = block.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = block.AddComponent<Rigidbody>();
                rb.isKinematic = true;
            }
        }
    }
}
