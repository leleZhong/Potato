using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using UnityEngine;

public class PlayerVoiceSetup : MonoBehaviour
{
    public Recorder recorder;
    public PhotonView photonView;

    void Start()
    {
        recorder = FindAnyObjectByType<Recorder>();
        if (photonView.IsMine)
            SetupRecorder();
    }

    void SetupRecorder()
    {
        if (PunVoiceClient.Instance.PrimaryRecorder == null)
        {
            PunVoiceClient.Instance.PrimaryRecorder = recorder;
            recorder.TransmitEnabled = true;
            Debug.Log("로컬 플레이어의 Recorder가 설정되었습니다.");
        }
        else
            Debug.Log("PrimaryRecorder가 이미 설정되어 있습니다.");
    }
}
