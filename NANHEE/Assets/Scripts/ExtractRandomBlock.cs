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
//        //���� �� ����

//        for (int i = 0; i < blocksLength; i++)
//        {
//            randomInt = random.Next(0, blocksLength);


//            if (selectedIndices.Contains(randomInt) == true)
//            {

//                //�ߺ����� �������� 6�� �������� �� ���� �� ������ �ؾ��Ѵ�
//            }
//            selectedIndices.Add(randomInt);
//            randomBlock = Instantiate(blocks[randomInt]);

//            //do
//            //{
//            //    randomInt = random.Next(0, blocksLength);

//            //} while (selectedIndices.Contains(randomInt) == true);

//            // selectedIndices.Add(randomInt); // ���õ� ��� �ε��� ����

//            //randomBlock = Instantiate(blocks[randomInt]);

//            //��ġ ����
//            randomBlock.transform.SetParent(blockPos);
//            randomBlock.transform.localPosition = Vector3.zero;
//            randomBlock.gameObject.SetActive(true);
//        }
        
        
            
        
        
//    }

    
//}
