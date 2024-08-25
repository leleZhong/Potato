using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialColor : MonoBehaviour
{
    public static TutorialColor Instance;

    [Header("[Puzzle]")]
    public GameObject[] _objects; // 오브젝트 4개

    public Texture[] _texturesRBGY; // 빨강 -> 파랑 -> 초록 -> 노랑 -> 빨강



    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        // 각 오브젝트에 TriggerEnter를 감지할 수 있도록 Collider가 있는지 확인하고 없으면 추가
        foreach (GameObject obj in _objects)
        {
            Collider collider = obj.GetComponent<Collider>();
            if (collider == null)
            {
                obj.AddComponent<BoxCollider>();
            }
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 오브젝트가 밟혔을 때 색을 변경
        for (int i = 0; i < _objects.Length; i++)
        {
            if (other.gameObject == _objects[i])
            {
                ChangeColor(_objects[i]);
                StageClear();
                break;
            }
        }
    }

    public void ChangeColor(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        int currentTextureIndex = System.Array.IndexOf(_texturesRBGY, renderer.material.mainTexture);
        renderer.material.mainTexture = _texturesRBGY[(currentTextureIndex + 1) % _texturesRBGY.Length];
    }

    public void StageClear()
    {
        bool isCorrect = true;

        for (int i = 0; i < _objects.Length; i++)
        {
            Renderer renderer = _objects[i].GetComponent<Renderer>();
            // 여기서는 정답 체크 로직이 없으므로, 단순히 Portal 활성화를 위한 논리적인 조건을 추가할 수 있습니다.
            if (renderer.material.mainTexture != _texturesRBGY[i]) // 이 조건은 임의로 설정한 것임
            {
                isCorrect = false;
                break;
            }
        }

        
    }
}
