using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum OrderNumber //타워의 정의 순서
{
    A,
    B,
    C,
    D,
    E,
    Null = -1
}
public class LoadRandomPrefabs : MonoBehaviour
{
    //public GameObject BlockGroup;
    //public List<Block> blocks = new List<Block>();

    //[HideInInspector]
    //public Block questBlock;

    public OrderNumber ordernumber;

    void Start()
    {
       
    }

    public void TowerOrderSequence() //어떤 타워를 먼저 정의할지
    {
        
    }
}
