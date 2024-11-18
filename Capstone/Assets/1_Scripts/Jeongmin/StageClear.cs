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

    public AudioSource doorReverbClip; // Door_Reverb_v1_wav ����� Ŭ��

    private bool stage1SoundPlayed = false;
    private bool stage2SoundPlayed = false;
    private bool stage3SoundPlayed = false;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (stage1clear && !stage1SoundPlayed)
        {
            photonView.RPC("OpenDoor", RpcTarget.All, 1);
            PlayDoorSound(); // ���� ���
            stage1SoundPlayed = true; // ���� �ߺ� ��� ����
        }
        if (stage2clear && !stage2SoundPlayed)
        {
            photonView.RPC("OpenDoor", RpcTarget.All, 2);
            PlayDoorSound(); // ���� ���
            stage2SoundPlayed = true; // ���� �ߺ� ��� ����
        }
        if (stage3clear && !stage3SoundPlayed)
        {
            photonView.RPC("OpenDoor", RpcTarget.All, 3);
            PlayDoorSound(); // ���� ���
            stage3SoundPlayed = true; // ���� �ߺ� ��� ����
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

    void PlayDoorSound()
    {
        if (doorReverbClip != null)
        {
            doorReverbClip.Play();
        }
    }
}
