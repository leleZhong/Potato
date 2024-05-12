using System.Collections.Generic;
using UnityEngine;

public class Prefpabslogic : MonoBehaviour
{
    public GameObject[] prefabs; // 프리펩들의 배열
    public int duplicateCount = 2; // 중복 선택할 프리펩의 수

    void Start()

        //TODO : 프리펩 로드, 위치 정하기
    {
        List<GameObject> selectedPrefabs = new List<GameObject>();

        // 중복을 포함하여 무작위로 프리펩 선택
        for (int i = 0; i < duplicateCount; i++)
        {
            GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];
            selectedPrefabs.Add(randomPrefab);
        }

        // 선택된 프리펩으로부터 게임 오브젝트를 생성
        foreach (var prefab in selectedPrefabs)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
