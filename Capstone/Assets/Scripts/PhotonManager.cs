using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        Screen.SetResolution(1280, 720, false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);

    public override void OnJoinedRoom()
    {
        // GameObject PI = PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);
    }
}
