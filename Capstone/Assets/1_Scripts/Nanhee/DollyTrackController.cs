using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyTrackController : MonoBehaviour
{
    public CinemachineDollyCart dollyCart; // Dolly Cart에 연결할 변수
    public float speed = 5f; // Dolly Cart의 이동 속도

    void Start()
    {
        // Scene이 시작될 때 DollyTrack이 움직이도록 설정
        dollyCart.m_Speed = speed;
    }
}
