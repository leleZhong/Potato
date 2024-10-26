using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StageClear : MonoBehaviour
{
    PhotonView photonView;

    public bool stage1clear = false;
    public bool stage2clear = false;
    public bool stage3clear = false;
    
    public Animator[] _door1;
    public Animator[] _door2;
    public Animator[] _door3;

    public GameObject _portal1;
    public GameObject _portal2;
    
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (stage1clear)
        {
            photonView.RPC("OpenDoor", RpcTarget.All, 1);
        }
        if (stage2clear)
        {
            photonView.RPC("OpenDoor", RpcTarget.All, 2);
        }
        if (stage3clear)
        {
            photonView.RPC("OpenDoor", RpcTarget.All, 3);
        }
    }

    [PunRPC]
    void OpenDoor(int doorNumber)
    {
        switch (doorNumber)
        {
            case 1:
                _door1[0].SetBool("isClear", true);
                _door1[1].SetBool("isClear", true);
                break;
            case 2:
                _door2[0].SetBool("isClear", true);
                _door2[1].SetBool("isClear", true);
                break;
            case 3:
                _door3[0].SetBool("isClear", true);
                _door3[1].SetBool("isClear", true);
                break;
        }
    }
}
