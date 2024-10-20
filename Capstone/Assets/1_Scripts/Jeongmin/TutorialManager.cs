using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Transform[] _spawnPoints;

    public void SpawnPlayers()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Photon에 연결되지 않았습니다.");
            return;
        }

        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber -1;
        playerIndex = Mathf.Clamp(playerIndex, 0, _spawnPoints.Length -1);

        Vector3 pos = _spawnPoints[playerIndex].position;
        Quaternion rot = _spawnPoints[playerIndex].rotation;

        PhotonNetwork.Instantiate("TutorialPlayer", pos, rot, 0);
    }
}
