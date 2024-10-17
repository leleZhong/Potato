using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour
{
    bool _isP1InPortal = false;
    bool _isP2InPortal = false;
    string _playerTag = "Player";

    public GameObject _portal1;
    public GameObject _portal2;

    public void EnteredPortal(GameObject portal, string tag, string sceneName)
    {
        if (portal == _portal1 && tag == _playerTag)
            _isP1InPortal = true;

        if (portal == _portal2 && tag == _playerTag)
            _isP2InPortal = true;

        CheckBothPlayersInPortal(sceneName);
    }

    void CheckBothPlayersInPortal(string sceneName)
    {
        if (_isP1InPortal && _isP2InPortal)
        {
            if (sceneName == "Tutorial")
                SceneManager.LoadScene(2);
            if (sceneName == "main stage")
                SceneManager.LoadScene(3);
        }
    }
}