using System.Collections;
using UnityEngine;

public class CanvasDelayActivator : MonoBehaviour
{
    public GameObject canvas;  // 캔버스를 참조할 변수

    void Start()
    {
        // 처음에 캔버스를 비활성화
        canvas.SetActive(false);

        // 10초 후에 캔버스를 활성화시키는 코루틴 실행
        StartCoroutine(ActivateCanvasAfterDelay(26f));
       
       

    }

    // 딜레이 후 캔버스를 활성화시키는 코루틴
    IEnumerator ActivateCanvasAfterDelay(float delay)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 캔버스 활성화
        canvas.SetActive(true);
    }
}
