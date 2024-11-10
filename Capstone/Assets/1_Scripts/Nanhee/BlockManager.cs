using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject[] blockPrefabs; // �� ������ �迭
    public Transform[] tower1Transforms; // Ÿ��1 ��ġ �迭
    public Transform[] tower2Transforms; // Ÿ��2 ��ġ �迭
    public static GameObject duplicatedBlockPrefab;
    private Collider objectCollider;
    public static int duplicatedBlockIndex;

    void Start()
    {
        InitializeBlocks();

        objectCollider = GetComponent<Collider>();
        if (objectCollider != null)
        {
            objectCollider.enabled = false; // �ݶ��̴� ��Ȱ��ȭ
        }
    }

    void InitializeBlocks()
    {
        List<int> availableNumbers = new List<int>() { 0, 1, 2, 3, 4 }; // ���� �ĺ� ��ȣ��

        int SelectedDuplicatedNumberIndex = Random.Range(0, availableNumbers.Count); //5 ������Ҵ��Ұ�?
        int SelectedDuplicatedNumber = availableNumbers[SelectedDuplicatedNumberIndex]; //����� �Ҵ�?
        availableNumbers.RemoveAt(SelectedDuplicatedNumberIndex); //�����鸮��Ʈ��������

        duplicatedBlockPrefab = blockPrefabs[SelectedDuplicatedNumber];
        duplicatedBlockIndex = SelectedDuplicatedNumber;

        int index1 = Random.Range(0, tower1Transforms.Length); //tower1�� 012�߿� ���?
        GameObject duplicatedBlock1 = Instantiate(blockPrefabs[SelectedDuplicatedNumber], tower1Transforms[index1].position, Quaternion.identity); //����
        duplicatedBlock1.transform.parent = tower1Transforms[index1];
        duplicatedBlock1.tag = "CorrectNumber";
        RemoveCollider(duplicatedBlock1);

        int index2 = Random.Range(0, tower2Transforms.Length); //tower2�� 012�߿� ���?
        GameObject duplicatedBlock2 = Instantiate(blockPrefabs[SelectedDuplicatedNumber], tower2Transforms[index2].position, Quaternion.identity); //����
        duplicatedBlock2.transform.parent = tower2Transforms[index2];
        duplicatedBlock2.tag = "CorrectNumber";
        RemoveCollider(duplicatedBlock2);
        Debug.Log(duplicatedBlock1);

        // Ÿ��1�� �����ϰ� �׸� �Ҵ�
        foreach (Transform cubeTransform in tower1Transforms)
        {
            if (cubeTransform == tower1Transforms[index1]) continue; // �̹� �ߺ� ����� �Ҵ�� ��ġ�� �ǳʶٱ�
            Debug.Log("Skipping duplicated block allocation at index: " + index1);

            int randomIndex = Random.Range(0, availableNumbers.Count); //tower1.0����ġ�� ����� �Ҵ��Ұ�?
            int selectedNumber = availableNumbers[randomIndex]; //����� �Ҵ�?

            GameObject block = Instantiate(blockPrefabs[selectedNumber], cubeTransform.position, Quaternion.identity); //���õ� ���������� ��ġ�� �Ҵ�
            block.transform.parent = cubeTransform;
            RemoveCollider(block);

            availableNumbers.RemoveAt(randomIndex); //�Ҵ�� ���ڸ� ����Ʈ���� �����
        }

        // Ÿ��2�� �����ϰ� �׸� �Ҵ�
        foreach (Transform cubeTransform in tower2Transforms)
        {
            if (cubeTransform == tower2Transforms[index2]) continue; // �̹� �ߺ� ����� �Ҵ�� ��ġ�� �ǳʶٱ�
            Debug.Log("Skipping duplicated block allocation at index: " + index1);

            int randomIndex = Random.Range(0, availableNumbers.Count); //����
            int selectedNumber = availableNumbers[randomIndex]; //�����Ѱ� ����Ʈ���� ��������(������Ҵ�?)

            GameObject block = Instantiate(blockPrefabs[selectedNumber], cubeTransform.position, Quaternion.identity); //����
            block.transform.parent = cubeTransform;
            RemoveCollider(block);

            availableNumbers.RemoveAt(randomIndex);
        }
    }

    void RemoveCollider(GameObject block)
    {
        Collider blockCollider = block.GetComponent<Collider>();
        if (blockCollider != null)
        {
            Destroy(blockCollider); // �ݶ��̴� ����
        }
    }
}
