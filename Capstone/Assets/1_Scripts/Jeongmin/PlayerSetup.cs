using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;

public class PlayerSetup : MonoBehaviour
{
    public PhotonView _pv;
    PhotonVoiceView _voiceView;

    Camera _camera;
    AudioListener _al;

    void Start()
    {
        _al = GetComponentInChildren<AudioListener>();
        _camera = GetComponentInChildren<Camera>();
        _voiceView = GetComponent<PhotonVoiceView>();

        if (PhotonNetwork.IsConnectedAndReady)
        {
                if (_pv.IsMine)
            {
                // ���� �÷��̾��� AudioListener Ȱ��ȭ
                if (_al != null)
                    _al.enabled = true;

                _camera.enabled = true;

                SetupPrimaryRecorder();
            }
            else
            {
                if (_al != null)
                    _al.enabled = false;
                
                _camera.enabled = false;
            }
            StartCoroutine(WaitForVoiceInitialization());
            StartCoroutine(CheckSpeakerSetupWithDelay());
        }
    }

    void Update()
    {
        if (_pv.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    GameObject clickedObject = hit.transform.gameObject;
                    ObjData objData = clickedObject.GetComponent<ObjData>();
                    
                    if (objData != null) // ObjData ????? ?? ??? Action? ??
                    {
                        GameManager.Instance.Action(clickedObject);
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_pv.IsMine)
        {
            if (other.transform.tag == "portal")
            {
                Debug.Log("Stage Clear");
                // SceneManager.LoadScene("nextStage");
            }
        }
    }

    void SetupPrimaryRecorder()
    {
        var recorder = GameObject.Find("RecorderObject").GetComponent<Recorder>();
        if (PunVoiceClient.Instance != null && recorder != null)
        {
            PunVoiceClient.Instance.PrimaryRecorder = recorder;
            recorder.TransmitEnabled = true;
            Debug.Log("PrimaryRecorder�� �����Ǿ����ϴ�.");
        }
    }

    IEnumerator CheckSpeakerSetupWithDelay()
    {
        yield return new WaitForSeconds(1f); // 1�� ���
        if (_voiceView.SpeakerInUse == null)
        {
            Debug.LogWarning("Speaker�� ������ �������� �ʾҽ��ϴ�.");
        }
        else
        {
            Debug.Log("Speaker�� �����Ǿ����ϴ�.");
        }
    }

    IEnumerator WaitForVoiceInitialization()
    {
        yield return new WaitForSeconds(1f); // 1�� ���
        if (PunVoiceClient.Instance.PrimaryRecorder != null)
        {
            PunVoiceClient.Instance.PrimaryRecorder.TransmitEnabled = true;
            Debug.Log("Voice initialization completed.");
        }
    }
}
