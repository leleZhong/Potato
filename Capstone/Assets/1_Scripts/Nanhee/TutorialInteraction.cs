using System.Collections;
using UnityEngine;

public class TutorialInteraction : MonoBehaviour
{
    public GameObject TutorialUI1; // 상호작용 UI
    public GameObject TutorialUI2;

    void Start()
    {
        TutorialUI1.SetActive(false); // 처음에 UI를 비활성화
        TutorialUI2.SetActive(false);

        // Coroutine으로 시간 기반 UI 제어
        StartCoroutine(ManageUI());
    }

    // UI 표시와 숨김을 시간에 따라 제어하는 Coroutine
    IEnumerator ManageUI()
    {
        // 2초 대기 후 TutorialUI1 표시
        yield return new WaitForSeconds(2f);
        TutorialUI1.SetActive(true);
        Debug.Log("TutorialUI1 Activated");

        // 8초 대기 후 TutorialUI1 숨김 (2초~10초 동안 활성화)
        yield return new WaitForSeconds(8f);
        TutorialUI1.SetActive(false);
        Debug.Log("TutorialUI1 Deactivated");

        // 10초 대기 후 TutorialUI2 표시 (20초부터 TutorialUI2 활성화)
        yield return new WaitForSeconds(10f);
        TutorialUI2.SetActive(true);
        Debug.Log("TutorialUI2 Activated");
    }
}
