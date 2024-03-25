using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public static ObjData _instance;
    int _id;
    bool _isNPC;
    
    void Awake()
    {
        _instance = this;
    }
}
