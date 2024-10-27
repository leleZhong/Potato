using UnityEngine;
using UnityEngine.SceneManagement;

public class gotomain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 20초 후에 LoadMainScene 메소드를 호출합니다.
        Invoke("LoadMainScene", 30f);
    }

    // Main 씬을 로드하는 메소드
    void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
