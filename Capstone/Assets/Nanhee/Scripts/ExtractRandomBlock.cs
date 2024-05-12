//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ExtractRandomBlock  : MonoBehaviour
//{
//    public LoadRandomPrefabs RandomPrefabsA;
//    public LoadRandomPrefabs RandomPrefabsB;

//    //public List<Blocks> blocks = new List<Blocks>();

//    public Transform blockPos;
//    public Blocks randomBlock;

    
    
    
    

//    private void Start()
//    {
//        Init();
//        //GetComponentInParent<OrderNumber>();
        
//    }

//    public void Init()
//    {
//        if (randomBlock != null)
//        {
//            Destroy(randomBlock);
//        }

//        System.Random random = new System.Random();
//        int blocksLength = blocks.Count;
//        int randomInt = random.Next(0, blocksLength);

//        List<int> selectedIndices = new List<int>();
//        //랜덤 블럭 지정

//        for (int i = 0; i < blocksLength; i++)
//        {
//            randomInt = random.Next(0, blocksLength);


//            if (selectedIndices.Contains(randomInt) == true)
//            {

//                //중복없는 랜덤값을 6개 만들어놓고 그 값에 블럭 대입을 해야한다
//            }
//            selectedIndices.Add(randomInt);
//            randomBlock = Instantiate(blocks[randomInt]);

//            //do
//            //{
//            //    randomInt = random.Next(0, blocksLength);

//            //} while (selectedIndices.Contains(randomInt) == true);

//            // selectedIndices.Add(randomInt); // 선택된 블록 인덱스 저장

//            //randomBlock = Instantiate(blocks[randomInt]);

//            //위치 지정
//            randomBlock.transform.SetParent(blockPos);
//            randomBlock.transform.localPosition = Vector3.zero;
//            randomBlock.gameObject.SetActive(true);
//        }
        
        
            
        
        
//    }

    
//}
