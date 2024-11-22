using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class StageManager : MonoBehaviour
{
    PhotonView photonView;

    // 퍼즐
    [Header("[Puzzle]")]
    public GameObject[] _objectsP1;
    public GameObject[] _objectsP2;
    public Texture[] _texturesRBGY; // 빨강 -> 파랑 -> 초록 -> 노랑 -> 빨강
    public Texture[] _texturesRGBY; // 빨강 -> 초록 -> 파랑 -> 노랑 -> 빨강
    public Texture[] _texturesRYBG; // 빨강 -> 노랑 -> 파랑 -> 초록 -> 빨강

    // 스테이지 클리어
    [Header("[Stage Clear]")]
    public StageClear stageClear;

    public Renderer[] _answerP1;
    public Renderer[] _answerP2;

    public bool isP1Correct = false;
    public bool isP2Correct = false;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (isP1Correct && isP2Correct)
        {
            photonView.RPC("SetStageClear", RpcTarget.All, true);
        }
    }

    [PunRPC]
    public void SetStageClear(bool isClear)
    {
        stageClear.stage1clear = isClear;
    }

    public void StageClear()
    {
        bool p1AllCorrect = true;
        bool p2AllCorrect = true;

        // P1 퍼즐 검사
        for (int i = 0; i < _objectsP1.Length; i++)
        {
            Renderer rendererP1 = _objectsP1[i].GetComponent<Renderer>();
            // 하나라도 정답이 아니면 p1AllCorrect를 false로 설정
            if (rendererP1.material.mainTexture != _answerP1[i].material.mainTexture)
            {
                p1AllCorrect = false;
                break;
            }
        }

        // P2 퍼즐 검사
        for (int i = 0; i < _objectsP2.Length; i++)
        {
            Renderer rendererP2 = _objectsP2[i].GetComponent<Renderer>();
            // 하나라도 정답이 아니면 p2AllCorrect를 false로 설정
            if (rendererP2.material.mainTexture != _answerP2[i].material.mainTexture)
            {
                p2AllCorrect = false;
                break;
            }
        }

        // 모든 타일이 정답일 때만 true 설정
        isP1Correct = p1AllCorrect;
        isP2Correct = p2AllCorrect;

        // 클리어 조건 확인
        if (isP1Correct && isP2Correct)
        {
            Debug.Log("Stage cleared!");
            photonView.RPC("SetStageClear", RpcTarget.All, true);
        }
    }

    void ChangeColor(GameObject[] objects, int[] indices, int step, Texture[] _textures)
    {
        foreach (int index in indices)
        {
            Renderer renderer = objects[index].GetComponent<Renderer>();
            int currentTextureIndex = System.Array.IndexOf(_textures, renderer.material.mainTexture);
            renderer.material.mainTexture = _textures[(currentTextureIndex + step) % _textures.Length];
        }
    }

    void ResetColor(GameObject[] objects)
    {
        Texture red = _texturesRBGY[0];

        foreach (GameObject obj in objects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            renderer.material.mainTexture = red;
        }
    }

    public void ButtonClick(int id)
    {
        switch (id)
        {
            // 리셋 버튼
            case 2001:
                ResetColor(_objectsP1);
                break;
            case 2002:
                ResetColor(_objectsP2);
                break;
                
            // P1 - 플레이어 1의 패턴
            // 정답 : 3-2-1-2-1(파노파초빨)
            case 3001:
                ChangeColor(_objectsP1, new int[] { 0, 4 }, 1, _texturesRYBG); // 원 1과 원 5
                break;
            case 3002:
                ChangeColor(_objectsP1, new int[] { 1, 3 }, 1, _texturesRBGY); // 원 2와 원 4
                break;
            case 3003:
                ChangeColor(_objectsP1, new int[] { 1, 2, 4 }, 1, _texturesRBGY); // 원 3과 원 4
                break;
            

            // P2 - 플레이어 2의 패턴
            // 정답 : 1-2-3-1-3(초파노파파)
            case 3004:
                ChangeColor(_objectsP2, new int[] { 0, 2 }, 1, _texturesRBGY); // 원 1과 원 3
                break;
            case 3005:
                ChangeColor(_objectsP2, new int[] { 0, 1, 3 }, 1, _texturesRGBY); // 원 2와 원 4
                break;
            case 3006:
                ChangeColor(_objectsP2, new int[] { 2, 4 }, 1, _texturesRBGY); // 원 3과 원 5
                break;
        }
        StageClear();
        GameManager.Instance._isAction = false;
    }
}
