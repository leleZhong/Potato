using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject MenuUI;
    public GameObject GameUI;
    public GameObject GameOverUI;

    void Start()
    {
        
    }

    public void GameStart()
    {
        MenuUI.SetActive(false);
        GameUI.SetActive(true);
    }

    public void GameOver()
    {
        GameUI.SetActive(false);
        GameOverUI.SetActive(true);

    }

    public void Restart()
    {
        SceneManager.LoadScene(1); //다시 시작하면 게임화면로드(1)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
