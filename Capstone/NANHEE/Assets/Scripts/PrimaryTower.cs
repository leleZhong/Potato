//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using Yeon;

//public class PrimaryTower : MonoBehaviour
//{


//    public Transform[] blockPos;
//    public GameObject[] blockPrefabs;
//    public SecondaryTower secondTower;


//    void Start()
//    {
//        Init();
//    }



//    public void Init()
//    {
//        List<int> availableNumbers = new List<int>() { 1,2,3,4,5,6 };
//        bool isDuplicated = false;

//        List<int> group1Numbers = new List<int>();

//        foreach(Transform cubeTransform in blockPos)
//        {
//            int randomIndex = Random.Range(0, availableNumbers.Count);
//            int selectedNumber = availableNumbers[randomIndex];

//            if(group1Numbers.Contains(selectedNumber))
//            {
//                isDuplicated = true;
//                availableNumbers.RemoveAll(num => group1Numbers.Contains(num));
//            }

//            group1Numbers.Add(selectedNumber);

//            GameObject block = Instantiate(blockPrefabs[selectedNumber - 1], cubeTransform.position, Quaternion.identity);
//            block.transform.parent = cubeTransform;
//        }

//        if (isDuplicated)
//        {
//            foreach (Transform cubeTransform in blockPos)
//            {
//                // 이미 할당된 번호 제거
//                group1Numbers.Remove(cubeTransform.GetComponentInChildren<Block>().GetBlockNumber());

//                int randomIndex = Random.Range(0, availableNumbers.Count);
//                int selectedNumber = availableNumbers[randomIndex];

//                // 할당된 번호 추가
//                group1Numbers.Add(selectedNumber);

//                // 블럭 할당 및 위치 설정
//                GameObject block = Instantiate(blockPrefabs[selectedNumber - 1], cubeTransform.position, Quaternion.identity);
//                block.transform.parent = cubeTransform;

//                // Debug.Log(cubeTransform.name + ": " + selectedNumber); // 번호 확인용 디버그 메시지
//            }
//        }

//        // 중복된 번호 전달
//        if (isDuplicated)
//        {
//            secondTower.ReceiveDuplicateNumber(group1Numbers[0]);
//        }
//    }
//}

        

    


    

