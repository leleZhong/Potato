using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool _isAction;

    public TalkManager talkManager;
    public Text _talkText;
    public GameObject _panel;
    public GameObject _scanObject;
    public int _talkIndex;

    public StageManager stageManager;

    void Awake()
    {
        Instance = this;
    }

    public void Action(GameObject scanObj)
    {
        
        _isAction = true;
        _scanObject = scanObj;
        ObjData objData = _scanObject.GetComponent<ObjData>();
        
        if (objData._isBtn)
        {
            stageManager.ChangeColor(objData._id);
        }
        else
        {
            Talk(objData._id);
            _panel.SetActive(_isAction);
        }
    }
    void Talk(int id)
    {
        string talkData = talkManager.GetTalk(id, _talkIndex);

        if (talkData == null)
        {
            _isAction = false;
            _talkIndex = 0; // 대화가 끝날 때 0으로 초기화
            _panel.SetActive(_isAction);
            return;
        }
        
        _talkText.text = talkData;
        
        _isAction = true;
        _talkIndex++;
    }
}
