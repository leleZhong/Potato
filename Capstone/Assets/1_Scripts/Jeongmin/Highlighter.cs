using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;
using Photon.Pun;

public class Highlighter : MonoBehaviour
{
    public PhotonView photonView;
    public PhotonVoiceView photonVoiceView; // PhotonVoiceView ������Ʈ ����
    public Image recorderSprite; // ���� �� ǥ�õǴ� ��������Ʈ
    public Image speakerSprite;  // ���� �� ǥ�õǴ� ��������Ʈ

    void Start()
    {
        // ���� �÷��̾��� ���� ���� ���¸� ǥ���ϵ��� �ʱ� ����
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
        // ���� �÷��̾��� ���� ���� ���� ǥ��
        if (photonView.IsMine)
        {
            recorderSprite.enabled = photonVoiceView.IsRecording;
            speakerSprite.enabled = photonVoiceView.IsSpeaking;
        }
    }
}
