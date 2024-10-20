using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public MainMenu _mainMenu;

    const string ReadyProperty = "IsReady";
    bool _isConnectedToMaster = false;

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
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject); // PhotonManager 객체�? ?��괴하�? ?��?���? ?��?��
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        _isConnectedToMaster = true;
    }

    public void JoinRoom()
    {
        if (_isConnectedToMaster)
            PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
        else
            Debug.LogError("마스터 서버에 연결되지 않았습니다.");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        // GameManager.Instance._isConnect = true;
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
            if (!player.CustomProperties.TryGetValue(ReadyProperty, out object isReady) || !(bool)isReady)
            {
                Debug.Log("Not all players are ready.");
                return;
            }
        }

        Debug.Log("All players ready. Starting game.");

        
        // GameManager.Instance.ConnectAndCreatePlayer();
        

        if (_mainMenu != null)
            {
                _mainMenu.OnLoadingFinish();
            }
    }
}
