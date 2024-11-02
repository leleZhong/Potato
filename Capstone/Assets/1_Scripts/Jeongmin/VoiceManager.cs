using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public Recorder recorder;

    void Start()
    {
        if (gameObject.GetPhotonView().IsMine)
        {
            if (recorder != null)
            {
                PunVoiceClient.Instance.PrimaryRecorder = recorder;
                recorder.TransmitEnabled = true;
            }
        }
    }
}
