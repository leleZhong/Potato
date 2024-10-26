using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject _gamePanel;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PhotonView pv = other.GetComponent<PhotonView>();

            if (pv.IsMine)
                _gamePanel.SetActive(true);
        }
    }
}
