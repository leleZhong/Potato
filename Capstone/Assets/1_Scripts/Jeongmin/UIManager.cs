using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Settings _settings;

    // [일시정지 ui]
    [Header("[Pause UI]")]
    public GameObject _pausePanel;

    void Start()
    {
        _pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pausePanel.activeSelf)
            {
                _pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                _pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void OnClickContinue()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }
}
