using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyTrackController : MonoBehaviour
{
    public CinemachineDollyCart dollyCart; // Dolly Cart�� ������ ����
    public float speed = 5f; // Dolly Cart�� �̵� �ӵ�

    void Start()
    {
        // Scene�� ���۵� �� DollyTrack�� �����̵��� ����
        dollyCart.m_Speed = speed;
    }
}
