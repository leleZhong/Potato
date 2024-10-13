using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
    public PhotonView _pv;
    Camera _camera;
    AudioListener _al;

    void Start()
    {
        _al = GetComponentInChildren<AudioListener>();
        _camera = GetComponentInChildren<Camera>();

        if (_pv.IsMine)
        {
            // 로컬 플레이어의 AudioListener 활성화
            if (_al != null)
                _al.enabled = true;

            _camera.enabled = true;
        }
        else
        {
            if (_al != null)
                _al.enabled = false;
            
            _camera.enabled = false;
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
}
