using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;  // 타임라인 제어를 위한 네임스페이스

public class TimeBlock : MonoBehaviour
{
    [SerializeField]
    public Transform characterBody;
    public Camera playerCamera;
    public Animator anim;  // 애니메이터 컴포넌트

    public PlayableDirector timeline;  // 타임라인을 제어하는 PlayableDirector

    private bool isInputEnabled = false;

    void Start()
    {
        // 타임라인이 끝날 때 애니메이터 상태 초기화
        if (timeline != null)
        {
            timeline.stopped += OnTimelineStopped;  // 타임라인 종료 이벤트 등록
        }

        StartCoroutine(DisableInputForSeconds(5));  // 입력 비활성화 코루틴
    }

    IEnumerator DisableInputForSeconds(float seconds)
    {
        isInputEnabled = false;
        yield return new WaitForSeconds(seconds);
        isInputEnabled = true;  // 입력 활성화
    }

    void Update()
    {
        if (isInputEnabled)
        {
            // 캐릭터 이동 및 기타 로직
        }
    }

    // 타임라인이 종료될 때 호출되는 함수
    void OnTimelineStopped(PlayableDirector director)
    {
        // 애니메이터 파라미터를 기본 상태로 초기화
        anim.SetBool("isRun", false);
        anim.SetBool("isWalk", false);
        anim.SetBool("isJump", false);
        anim.ResetTrigger("doJump");

        Debug.Log("Timeline has ended. Animator parameters reset to default.");
    }
}
