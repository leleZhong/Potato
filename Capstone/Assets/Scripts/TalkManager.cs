using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    List<string> _talkData;

    void Start()
    {
        _talkData = new List<string>();
        GenerateData();
    }

    void GenerateData()
    {
        
    }
}
