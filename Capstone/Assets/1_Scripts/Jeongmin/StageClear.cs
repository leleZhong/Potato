using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    public bool stage1clear = false;
    public bool stage2clear = false;
    public bool stage3clear = false;
    
    public Animator[] _door1;
    public Animator[] _door2;
    public Animator[] _door3;

    public GameObject _portal1;
    public GameObject _portal2;
    
    void Update()
    {
        if (stage1clear)
        {
            _door1[0].SetBool("isClear", true);
            _door1[1].SetBool("isClear", true);
        }
        if (stage2clear)
        {
            _door2[0].SetBool("isClear", true);
            _door2[1].SetBool("isClear", true);
        }
        if (stage3clear)
        {
            _door3[0].SetBool("isClear", true);
            _door3[1].SetBool("isClear", true);
        }
    }
}
