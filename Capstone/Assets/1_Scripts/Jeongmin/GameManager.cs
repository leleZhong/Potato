using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool _isConnect = false;
    public Transform[] _spawnPoints;

    public bool _isAction;

    public TalkManager talkManager;
    public Text _talkText;
    public GameObject _panel;
    public GameObject _scanObject;
    public int _talkIndex;

    public StageManager stageManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
            Destroy(this.gameObject);

        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();
        if (eventSystems.Length > 1)
        {
            for (int i = 1; i < eventSystems.Length; i++)  // ? ?? ??? ???? ??? ??? ??
            {
                Destroy(eventSystems[i].gameObject);
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ?? ??? ? StageManager? ?? ?? ???
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // StageManager? null? ?? ??? ??
        if (stageManager == null)
        {
            stageManager = FindObjectOfType<StageManager>();

            if (stageManager != null)
            {
                Debug.Log("StageManager? ???????.");
            }
            else
            {
                Debug.LogError("StageManager? ?? ?????.");
            }
        }
    }

    // ????? SceneManager? ???? ???? ???
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void Initialize()
    {
        ConnectAndCreatePlayer();
    }

    public void ConnectAndCreatePlayer()
    {
        Debug.Log("ConnectAndCreatePlayer");
        StartCoroutine(CreatePlayer());
    }

    IEnumerator CreatePlayer()
    {
        Debug.Log("Waiting for connection...");
        yield return new WaitUntil(() => _isConnect);

        _spawnPoints[0] = GameObject.Find("JM_P1").transform.Find("SpawnPoint1").transform;
        _spawnPoints[1] = GameObject.Find("JM_P2").transform.Find("SpawnPoint2").transform;

        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        playerIndex = Mathf.Clamp(playerIndex, 0, _spawnPoints.Length - 1);

        Vector3 pos = _spawnPoints[playerIndex].position;
        Quaternion rot = _spawnPoints[playerIndex].rotation;

        GameObject playerTemp = PhotonNetwork.Instantiate("character1", pos, rot, 0);
    }

    public void Action(GameObject scanObj)
    {
        
        _isAction = true;
        _scanObject = scanObj;
        ObjData objData = _scanObject.GetComponent<ObjData>();
        ObjData.GameObjectTypes type = objData._type;

        switch (type)
        {
            case ObjData.GameObjectTypes.NPC:
                Talk(objData._id);
                _panel.SetActive(_isAction);
                break;
            case ObjData.GameObjectTypes.Button:
                stageManager.ButtonClick(objData._id);
                break;
        }
    }
    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(id, _talkIndex);

        if (talkData == null)
        {
            _isAction = false;
            _talkIndex = 0; // ????ôîÍ∞? ?Åù?Ç† ?ïå 0?úºÎ°? Ï¥àÍ∏∞?ôî
            _panel.SetActive(_isAction);
            return;
        }
        
        _talkText.text = talkData;
        
        _isAction = true;
        _talkIndex++;
    }
}
