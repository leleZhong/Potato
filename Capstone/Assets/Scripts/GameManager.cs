using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public TalkManager talkManager;
    public Text _talkText;
    public GameObject _panel;
    public GameObject _scanObject;
    public bool _isAction;
    int _talkIndex;

    void Awake()
    {
        _instance = this;
    }

    public void Action(GameObject scanObj)
    {
        if (_isAction) // Exit Action
        {
            _isAction = false;
        } else  // Enter Action
        {
            _panel.SetActive(true);
            _scanObject = scanObj;
            
            ObjData objData = _scanObject.GetComponent<ObjData>();
            TalkManager(objData.IsDestroyed, objData.isNPC);
        }
        _panel.SetActive(_isAction);
    }
    void Talk(int id, bool isNPC)
    {
        string talkData = talkManager.GetTalk(id, _talkIndex);

        if (talkData == null)
        {
            _isAction = false;
            _talkIndex = 0; // 대화가 끝날 때 0으로 초기화
            return;
        }

        if (isNPC)
        {
            _talkText.text = talkData;
        } else
        {
            _talkText.text = talkData;
        }

        _isAction = true;
        _talkIndex++;
    }
}
