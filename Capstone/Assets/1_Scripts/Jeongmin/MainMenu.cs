using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PhotonManager _photonManager;

    // [시작 화면 ui]
    [Header("[Start UI]")]
    public Text _title;
    public Text _loading;

    void Start()
    {
        _loading.gameObject.SetActive(false);
    }

    public void OnClickStart()
    {
        _title.gameObject.SetActive(false);
        _loading.gameObject.SetActive(true);
        StartCoroutine(ShowLoadingText());

        _photonManager._mainMenu = this; // PhotonManager에 UIManager를 연결
        _photonManager.JoinRoom();
        _photonManager.SetPlayerReady();
    }

    IEnumerator ShowLoadingText()
    {
        while (true)
        {
            string message = "Loading...";
            int index = 0;
            while (_loading.gameObject.activeSelf)
            {
                _loading.text = message.Substring(0, index + 1);
                index = (index + 1) % message.Length; // 인덱스 순환
                _photonManager.CheckAllPlayersReady();
                yield return new WaitForSeconds(0.3f); // 딜레이
            }
        }
    }

    public void OnLoadingFinish()
    {
        Debug.Log("OnLoadingFinish");
        _loading.gameObject.SetActive(false);
        SceneManager.LoadScene("Jeongmin");
    }

    public void OnClickExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
