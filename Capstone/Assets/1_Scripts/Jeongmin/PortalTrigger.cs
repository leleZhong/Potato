using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    public PortalManager _pm;
    
    string _sceneName;

    void Awake()
    {
        _sceneName = SceneManager.GetActiveScene().name;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PhotonView pv = other.GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                int playerNumber = pv.Owner.ActorNumber; // 플레이어 넘버를 가져옴
                _pm.EnteredPortal(gameObject, playerNumber, _sceneName);
            }
        }
    }
}
