using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int blockNumber; // ��� ��ȣ�� �����ϴ� ����

    // ����� ��ȣ�� �����ϴ� �Լ�
    public void SetBlockNumber(int number)
    {
        blockNumber = number;
    }

    // ����� ��ȣ�� ��ȯ�ϴ� �Լ�
    public int GetBlockNumber()
    {
        return blockNumber;
    }
}