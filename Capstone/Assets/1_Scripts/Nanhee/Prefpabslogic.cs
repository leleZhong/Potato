using System.Collections.Generic;
using UnityEngine;

public class Prefpabslogic : MonoBehaviour
{
    public GameObject[] prefabs; // ��������� �迭
    public int duplicateCount = 2; // �ߺ� ������ �������� ��

    void Start()

        //TODO : ������ �ε�, ��ġ ���ϱ�
    {
        List<GameObject> selectedPrefabs = new List<GameObject>();

        // �ߺ��� �����Ͽ� �������� ������ ����
        for (int i = 0; i < duplicateCount; i++)
        {
            GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];
            selectedPrefabs.Add(randomPrefab);
        }

        // ���õ� ���������κ��� ���� ������Ʈ�� ����
        foreach (var prefab in selectedPrefabs)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
