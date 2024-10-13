using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    public PortalManager _pm;
    
    string _sceneName;

    void Awake()
    {
        _sceneName = SceneManager.GetActiveScene().name;
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _pm.EnteredPortal(gameObject, other.tag, _sceneName);
        }
    }
}
