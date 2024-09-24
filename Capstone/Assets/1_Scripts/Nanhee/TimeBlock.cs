using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;  // Ÿ�Ӷ��� ��� ���� ���ӽ����̽�

public class TimeBlock : MonoBehaviour
{
    [SerializeField]
    public Transform characterBody;
    public Camera playerCamera;
    public Animator anim;  // �ִϸ����� ������Ʈ

    public PlayableDirector timeline;  // Ÿ�Ӷ����� �����ϴ� PlayableDirector

    private bool isInputEnabled = false;

    void Start()
    {
        // Ÿ�Ӷ����� ���� �� �ִϸ����� ���� �ʱ�ȭ
        if (timeline != null)
        {
            timeline.stopped += OnTimelineStopped;  // Ÿ�Ӷ��� ���� �̺�Ʈ ���
        }

        StartCoroutine(DisableInputForSeconds(5));  // �Է� ��Ȱ��ȭ �ڷ�ƾ
    }

    IEnumerator DisableInputForSeconds(float seconds)
    {
        isInputEnabled = false;
        yield return new WaitForSeconds(seconds);
        isInputEnabled = true;  // �Է� Ȱ��ȭ
    }

    void Update()
    {
        if (isInputEnabled)
        {
            // ĳ���� �̵� �� ��Ÿ ����
        }
    }

    // Ÿ�Ӷ����� ����� �� ȣ��Ǵ� �Լ�
    void OnTimelineStopped(PlayableDirector director)
    {
        // �ִϸ����� �Ķ���͸� �⺻ ���·� �ʱ�ȭ
        anim.SetBool("isRun", false);
        anim.SetBool("isWalk", false);
        anim.SetBool("isJump", false);
        anim.ResetTrigger("doJump");

        Debug.Log("Timeline has ended. Animator parameters reset to default.");
    }
}
