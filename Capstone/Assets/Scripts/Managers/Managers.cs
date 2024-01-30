using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성 보장(Singleton Pattern)
    static Managers Instance{ get { Init(); return s_instance; } }  // 유일한 인스턴스를 갖고 옴

    InputManager _input = new InputManager();

    public static InputManager Input { get { return Instance._input; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    // 인스턴스 초기화
    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            
            DontDestroyOnLoad(go);

            s_instance = go.GetComponent<Managers>();
        }
    }
}
