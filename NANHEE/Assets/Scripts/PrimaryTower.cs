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
//                // �̹� �Ҵ�� ��ȣ ����
//                group1Numbers.Remove(cubeTransform.GetComponentInChildren<Block>().GetBlockNumber());

//                int randomIndex = Random.Range(0, availableNumbers.Count);
//                int selectedNumber = availableNumbers[randomIndex];

//                // �Ҵ�� ��ȣ �߰�
//                group1Numbers.Add(selectedNumber);

//                // �� �Ҵ� �� ��ġ ����
//                GameObject block = Instantiate(blockPrefabs[selectedNumber - 1], cubeTransform.position, Quaternion.identity);
//                block.transform.parent = cubeTransform;

//                // Debug.Log(cubeTransform.name + ": " + selectedNumber); // ��ȣ Ȯ�ο� ����� �޽���
//            }
//        }

//        // �ߺ��� ��ȣ ����
//        if (isDuplicated)
//        {
//            secondTower.ReceiveDuplicateNumber(group1Numbers[0]);
//        }
//    }
//}

        

    


    

