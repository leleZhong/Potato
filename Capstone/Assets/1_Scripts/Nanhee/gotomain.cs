using UnityEngine;
using UnityEngine.SceneManagement;

public class gotomain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 20�� �Ŀ� LoadMainScene �޼ҵ带 ȣ���մϴ�.
        Invoke("LoadMainScene", 30f);
    }

    // Main ���� �ε��ϴ� �޼ҵ�
    void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
