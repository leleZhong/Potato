using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
    public PhotonView _pv;
    AudioListener _al;

    void Start()
    {
        _al = GetComponentInChildren<AudioListener>();

        if (_pv.IsMine)
        {
            // 로컬 플레이어의 AudioListener 활성화
            if (_al != null)
                _al.enabled = true;
        }
        else
        {
            if (_al != null)
                _al.enabled = false;
        }
    }
}
