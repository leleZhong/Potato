using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    // 한 번 클릭할 때마다 모든 오브젝트들의 텍스쳐가 바뀜
    // 3001번 버튼 : 빨-파-초-노-빨 1단계씩
    // 3002번 버튼 : 첫 번째, 세 번째 오브젝트의 텍스쳐가 1단계씩 바뀜

    public GameObject[] _objects;
    public Texture[] _textures;
    public void ChangeColor(int id)
    {
        switch (id)
        {
            case 3001:
                for (int i = 0; i < _objects.Length; i++)
                {
                    if (i == 2 || i == 4)
                    {
                        Renderer renderer = _objects[i].GetComponent<Renderer>();
                        int currentTextureIndex = System.Array.IndexOf(_textures, renderer.material.mainTexture);
                        renderer.material.mainTexture = _textures[(currentTextureIndex + 1) % _textures.Length];
                    }
                }
                GameManager.Instance._isAction = false;
                break;
            case 3002:
                for (int i = 0; i < _objects.Length; i++)
                {
                    if (i == 0 || i == 2)
                    {
                        Renderer renderer = _objects[i].GetComponent<Renderer>();
                        int currentTextureIndex = System.Array.IndexOf(_textures, renderer.material.mainTexture);
                        renderer.material.mainTexture = _textures[(currentTextureIndex + 1) % _textures.Length];
                    }
                }
                GameManager.Instance._isAction = false;
                break;
            case 3003:
                for (int i = 0; i < _objects.Length; i++)
                {
                    if (i == 2 || i == 3)
                    {
                        Renderer renderer = _objects[i].GetComponent<Renderer>();
                        int currentTextureIndex = System.Array.IndexOf(_textures, renderer.material.mainTexture);
                        renderer.material.mainTexture = _textures[(currentTextureIndex + 3) % _textures.Length];
                    }
                }
                GameManager.Instance._isAction = false;
                break;
        }
    }
}
