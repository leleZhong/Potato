using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public MainMenu _mainMenu;

    const string ReadyProperty = "IsReady";

    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);

        if (PhotonNetwork.IsConnected)
        {
            Debug.LogWarning("Photon is already connected.");
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        DontDestroyOnLoad(gameObject); // PhotonManager 객체를 파괴하지 않도록 설정
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
        CheckAllPlayersReady();
    }

    public void SetPlayerReady()
    {
        Hashtable props = new Hashtable
        {
            { ReadyProperty, true }
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        Debug.Log("SetPlayerReady");
        CheckAllPlayersReady();
    }
    public void CheckAllPlayersReady()
    {
        Debug.Log("Checking if all players are ready.");

        if (PhotonNetwork.CurrentRoom.Players.Count != 2)
        {
            Debug.Log("Not enough players in room.");
            return;
        }

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.TryGetValue(ReadyProperty, out object isReady) && (bool)isReady)
            {
                continue;
            }
        }

        Debug.Log("All players ready. Starting game.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance._isConnect = true;
            GameManager.Instance.ConnectAndCreatePlayer();
        }

        if (_mainMenu != null)
            {
                _mainMenu.OnLoadingFinish();
            }
    }
}
