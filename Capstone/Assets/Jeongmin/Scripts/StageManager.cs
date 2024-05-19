using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    // 한 번 클릭할 때마다 모든 오브젝트들의 텍스쳐가 바뀜
    // 3001번 버튼 : 빨-파-초-노-빨 1단계씩
    // 3002번 버튼 : 첫 번째, 세 번째 오브젝트의 텍스쳐가 1단계씩 바뀜

    public static StageManager Instance;

    // 퍼즐
    public GameObject[] _objectsP1;
    public GameObject[] _objectsP2;
    public Texture[] _textures;

    // 스테이지 클리어
    public Renderer[] _answerP1;
    public Renderer[] _answerP2;
    public GameObject _portal;
    public GameObject _portalLight;

    void Awake()
    {
        Instance = this;

        _portalLight.SetActive(false);
        _portal.GetComponent<SphereCollider>().enabled = false;
    }

    public void StageClear()
    {
        Renderer[] rendererP1 = new Renderer[_objectsP1.Length];
        Renderer[] rendererP2 = new Renderer[_objectsP2.Length];

        for (int i = 0; i < _objectsP1.Length; i++)
        {
            rendererP1[i] = _objectsP1[i].GetComponent<Renderer>();
            rendererP2[i] = _objectsP2[i].GetComponent<Renderer>();

        }

        if (_answerP1 == rendererP1 && _answerP2 == rendererP2)
        {
            _portalLight.SetActive(true);
            _portal.GetComponent<SphereCollider>().enabled = true;
            
        }
        Debug.Log("Stage Clear 호출");
    }

    public void ChangeColor(GameObject[] objects, int[] indices, int step)
    {
        foreach (int index in indices)
        {
            Renderer renderer = objects[index].GetComponent<Renderer>();
            int currentTextureIndex = System.Array.IndexOf(_textures, renderer.material.mainTexture);
            renderer.material.mainTexture = _textures[(currentTextureIndex + step) % _textures.Length];
        }
    }

    public void ButtonClick(int id)
    {
        switch (id)
        {
            // P1
            case 3001:
                ChangeColor(_objectsP1, new int[] { 0, 1, 2, 3, 4 }, 1);
                break;
            case 3002:
                ChangeColor(_objectsP1, new int[] { 0, 1, 2, 3, 4 }, 1);
                break;
            case 3003:
                ChangeColor(_objectsP1, new int[] { 0, 1, 2, 3, 4 }, 1);
                break;

            // P2
            case 3004:
                ChangeColor(_objectsP1, new int[] { 0, 1, 2, 3, 4 }, 1);
                break;
            case 3005:
                ChangeColor(_objectsP1, new int[] { 0, 1, 2, 3, 4 }, 1);
                break;
            case 3006:
                ChangeColor(_objectsP1, new int[] { 0, 1, 2, 3, 4 }, 1);
                break;
        }
        StageClear();
        GameManager.Instance._isAction = false;
    }
}
