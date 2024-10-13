using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    bool stage1clear = false;
    bool stage2clear = false;
    bool stage3clear = false;
    
    public Animator[] _doors;
    public GameObject _portal1;
    public GameObject _portal2;

    void Awake()
    {
        _portal1.GetComponent<SphereCollider>().enabled = false;
        _portal2.GetComponent<SphereCollider>().enabled = false;
    }
    
    void Update()
    {
        if (stage1clear)
            _doors[0].SetBool("open", true);
        if (stage2clear)
            _doors[1].SetBool("open", true);
        if (stage3clear)
        {
            _doors[2].SetBool("open", true);
            
            _portal1.GetComponent<SphereCollider>().enabled = true;
            _portal2.GetComponent<SphereCollider>().enabled = true;
        }
    }
}
