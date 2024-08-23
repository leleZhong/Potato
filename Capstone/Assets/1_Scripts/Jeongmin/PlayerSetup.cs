using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
    public PhotonView _pv;
    public Camera _camera;
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

    void Update()
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
