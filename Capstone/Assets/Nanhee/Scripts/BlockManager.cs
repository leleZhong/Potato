using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject[] blockPrefabs; // 블럭 프리팹 배열
    public Transform[] tower1Transforms; // 타워1 위치 배열
    public Transform[] tower2Transforms; // 타워2 위치 배열
    public static GameObject duplicatedBlockPrefab;
    private Collider objectCollider;

    void Start()
    {
        InitializeBlocks();

        objectCollider = GetComponent<Collider>();
        if (objectCollider != null)
        {
            objectCollider.enabled = false; // 콜라이더 비활성화
        }
    }

    void InitializeBlocks()
    {
        List<int> availableNumbers = new List<int>() { 0, 1, 2, 3, 4 }; // 생성 후보 번호들

        //List<int> tower1ListNumber = new List<int>(); // 타워1에 할당된 번호들
        //List<int> tower2ListNumber = new List<int>(); // 타워2에 할당된 번호들

        // 랜덤한 번호 선택(중복할당용) -> 할당 -> 생성
        int SelectedDuplicatedNumberIndex = Random.Range(0, availableNumbers.Count); //5 몇번을할당할거?
        int SelectedDuplicatedNumber = availableNumbers[SelectedDuplicatedNumberIndex]; //몇번을 할당?
        availableNumbers.RemoveAt(SelectedDuplicatedNumberIndex); //프리펩리스트에서지움

        //태그 달아주기


        duplicatedBlockPrefab = blockPrefabs[SelectedDuplicatedNumber];


        int index1 = Random.Range(0, tower1Transforms.Length); //tower1의 012중에 어디?
        GameObject duplicatedBlock1 = Instantiate(blockPrefabs[SelectedDuplicatedNumber], tower1Transforms[index1].position, Quaternion.identity); //생성
        duplicatedBlock1.transform.parent = tower1Transforms[index1];
        duplicatedBlock1.tag = "CorrectNumber";

        // 타워2에 중복 블록 할당
        int index2 = Random.Range(0, tower2Transforms.Length); //tower2의 012중에 어디?
        GameObject duplicatedBlock2 = Instantiate(blockPrefabs[SelectedDuplicatedNumber], tower2Transforms[index2].position, Quaternion.identity); //생성
        duplicatedBlock2.transform.parent = tower2Transforms[index2];
        duplicatedBlock2.tag = "CorrectNumber";


        // 타워1에 랜덤하게 그림 할당
        foreach (Transform cubeTransform in tower1Transforms)
        {
            if (cubeTransform == tower1Transforms[index1]) continue; // 이미 중복 블록이 할당된 위치는 건너뛰기
            Debug.Log("Skipping duplicated block allocation at index: " + index1);

            int randomIndex = Random.Range(0, availableNumbers.Count); //tower1.0번위치에 몇번을 할당할거?
            int selectedNumber = availableNumbers[randomIndex]; //몇번을 할당?
                                                                //tower1ListNumber.Add(selectedNumber);//리스트에 넣기

            GameObject block = Instantiate(blockPrefabs[selectedNumber], cubeTransform.position, Quaternion.identity); //선택된 블럭프리펩을 위치에 할당
            block.transform.parent = cubeTransform;


            availableNumbers.RemoveAt(randomIndex); //할당된 숫자를 리스트에서 지우기

        }

        // 타워2에 랜덤하게 그림 할당
        foreach (Transform cubeTransform in tower2Transforms)
        {
            if (cubeTransform == tower2Transforms[index2]) continue; // 이미 중복 블록이 할당된 위치는 건너뛰기
            Debug.Log("Skipping duplicated block allocation at index: " + index1);

            int randomIndex = Random.Range(0, availableNumbers.Count); //랜뽑
            int selectedNumber = availableNumbers[randomIndex]; //랜뽑한걸 리스트에서 꺼내오기(몇번을할당?)
                                                                //tower2ListNumber.Add(selectedNumber);

            GameObject block = Instantiate(blockPrefabs[selectedNumber], cubeTransform.position, Quaternion.identity); //생성
            block.transform.parent = cubeTransform;


            availableNumbers.RemoveAt(randomIndex);

        }
    }
}
