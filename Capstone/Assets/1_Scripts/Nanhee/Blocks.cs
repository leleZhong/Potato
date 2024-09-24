using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int blockNumber; // 블록 번호를 저장하는 변수

    // 블록의 번호를 설정하는 함수
    public void SetBlockNumber(int number)
    {
        blockNumber = number;
    }

    // 블록의 번호를 반환하는 함수
    public int GetBlockNumber()
    {
        return blockNumber;
    }
}