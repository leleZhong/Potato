using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject _gamePanel;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            _gamePanel.SetActive(true);
    }
}
