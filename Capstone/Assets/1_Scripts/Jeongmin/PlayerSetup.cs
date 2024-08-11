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
            // ���� �÷��̾��� AudioListener Ȱ��ȭ
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
