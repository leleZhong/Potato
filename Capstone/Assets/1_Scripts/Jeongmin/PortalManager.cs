using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PortalManager : MonoBehaviour
{
    public bool _isP1InPortal = false;
    public bool _isP2InPortal = false;
    string _playerTag = "Player";
    string _sceneName;

    public GameObject _portal1;
    public GameObject _portal2;

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (_sceneName != null)
            CheckBothPlayersInPortal(_sceneName);
    }

    public void EnteredPortal(GameObject portal, int playerNumber, string sceneName)
    {
        if (playerNumber == 1)
            _isP1InPortal = true;
        else if (playerNumber == 2)
            _isP2InPortal = true;

        _sceneName = sceneName;

        // 모든 클라이언트에 상태를 동기화하기 위해 RPC 호출
        photonView.RPC("SyncPortalState", RpcTarget.All, _isP1InPortal, _isP2InPortal, _sceneName);
    }

    [PunRPC]
    void SyncPortalState(bool p1InPortal, bool p2InPortal, string sceneName)
    {
        _isP1InPortal = p1InPortal;
        _isP2InPortal = p2InPortal;
        _sceneName = sceneName;
    }

    void CheckBothPlayersInPortal(string sceneName)
    {
        if (_isP1InPortal && _isP2InPortal)
        {
            if (sceneName == "Tutorial")
                SceneManager.LoadScene("main stage");
            if (sceneName == "main stage")
                SceneManager.LoadScene("Cinematic");
        }
    }
}
