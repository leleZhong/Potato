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
            Debug.Log("���� �÷��̾��� Recorder�� �����Ǿ����ϴ�.");
        }
        else
            Debug.Log("PrimaryRecorder�� �̹� �����Ǿ� �ֽ��ϴ�.");
    }
}
