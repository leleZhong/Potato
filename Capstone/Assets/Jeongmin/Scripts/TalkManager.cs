using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public Dictionary<int, string[]> _talkData;

    void Awake()
    {
        _talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        _talkData.Add(1000, new string[] { "NPC text1", "NPC text2" });
        _talkData.Add(0, new string[] { "Obj text" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex >= _talkData[id].Length)  // 남아있는 데이터가 있는지
        {
            return null;
        } else
        {
            return _talkData[id][talkIndex];
        }
    }
}
