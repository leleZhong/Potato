using System.Collections;
using UnityEngine;

public class InputDisabler : MonoBehaviour
{
    private bool isInputBlocked = false; // 입력이 차단되었는지 확인하는 변수
    public float blockDuration = 3.0f; // 입력을 차단할 시간 (초)

    void Update()
    {
        if (isInputBlocked)
        {
            // 입력 차단 중일 때, 키보드와 마우스 입력을 무시함
            return;
        }

        // 입력 처리
        HandleInput();
    }

    // 외부에서 호출할 수 있는 입력 차단 함수
    public void BlockInput(float duration)
    {
        if (!isInputBlocked)
        {
            StartCoroutine(BlockInputForDuration(duration));
        }
    }

    // 마우스와 키보드 입력을 처리하는 함수 (임시 예시)
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W pressed");
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button pressed");
        }
        // 기타 입력 처리 코드 추가 가능
    }

    // 입력 차단을 처리하는 코루틴
    private IEnumerator BlockInputForDuration(float duration)
    {
        isInputBlocked = true; // 입력 차단 상태로 설정

        yield return new WaitForSeconds(duration); // 지정된 시간 동안 대기

        isInputBlocked = false; // 입력 차단 해제
    }
}
