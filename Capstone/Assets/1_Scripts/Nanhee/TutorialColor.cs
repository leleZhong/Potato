using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialColor : MonoBehaviour
{
    public static TutorialColor Instance;

    [Header("[Puzzle]")]
    public GameObject[] _objects; // ������Ʈ 4��

    public Texture[] _texturesRBGY; // ���� -> �Ķ� -> �ʷ� -> ��� -> ����



    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        // �� ������Ʈ�� TriggerEnter�� ������ �� �ֵ��� Collider�� �ִ��� Ȯ���ϰ� ������ �߰�
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
        // ������Ʈ�� ������ �� ���� ����
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
            // ���⼭�� ���� üũ ������ �����Ƿ�, �ܼ��� Portal Ȱ��ȭ�� ���� ������ ������ �߰��� �� �ֽ��ϴ�.
            if (renderer.material.mainTexture != _texturesRBGY[i]) // �� ������ ���Ƿ� ������ ����
            {
                isCorrect = false;
                break;
            }
        }

        
    }
}
