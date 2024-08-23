using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // public Rigidbody _rd;
    public PhotonView _pv;
    // public Animator _anim; 

    public float _speed;

    // IEnumerator Start()
    // {
    //     yield return new WaitForSeconds(0.5f);

    //     if (_pv.IsMine)
    //     {
    //         Camera.main.GetComponent<CameraController>()._target = transform.Find("CamPivot").transform;
    //     }
    //     else
    //     {
    //         _rd.isKinematic = true;
    //     }
    // }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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

    // void FixedUpdate()
    // {
    //     float h = GameManager.Instance._isAction ? 0 : Input.GetAxisRaw("Horizontal");
    //     float v = GameManager.Instance._isAction ? 0 : Input.GetAxisRaw("Vertical");
    //     Vector3 nextPos = new Vector3(h, 0, v) * _speed * Time.deltaTime;
    //     Vector3 currentPos = transform.position + nextPos;

    //     transform.position = currentPos;

    //     if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
    //     {
    //         _anim.SetBool("Input", true);
    //     }
    //     else
    //     {
    //         _anim.SetBool("Input", false);
    //     }
    // }

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
