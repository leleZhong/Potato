using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;
using Photon.Pun;

public class Highlighter : MonoBehaviour
{
    public PhotonView photonView;
    public PhotonVoiceView photonVoiceView; // PhotonVoiceView 컴포넌트 참조
    public Image recorderSprite; // 말할 때 표시되는 스프라이트
    public Image speakerSprite;  // 들을 때 표시되는 스프라이트

    void Start()
    {
        // 로컬 플레이어일 때만 녹음 상태를 표시하도록 초기 설정
        if (photonView.IsMine)
        {
            recorderSprite.enabled = false;
            speakerSprite.enabled = false;
        }
        else
            gameObject.SetActive(false);
    }

    private void Update()
    {
        // 로컬 플레이어일 때는 녹음 상태 표시
        if (photonView.IsMine)
        {
            recorderSprite.enabled = photonVoiceView.IsRecording;
            speakerSprite.enabled = photonVoiceView.IsSpeaking;
        }
    }
}
