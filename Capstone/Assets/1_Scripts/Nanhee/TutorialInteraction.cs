using System.Collections;
using UnityEngine;

public class TutorialInteraction : MonoBehaviour
{
    public GameObject TutorialUI1; // ��ȣ�ۿ� UI
    public GameObject TutorialUI2;

    void Start()
    {
        TutorialUI1.SetActive(false); // ó���� UI�� ��Ȱ��ȭ
        TutorialUI2.SetActive(false);

        // Coroutine���� �ð� ��� UI ����
        StartCoroutine(ManageUI());
    }

    // UI ǥ�ÿ� ������ �ð��� ���� �����ϴ� Coroutine
    IEnumerator ManageUI()
    {
        // 2�� ��� �� TutorialUI1 ǥ��
        yield return new WaitForSeconds(2f);
        TutorialUI1.SetActive(true);
        Debug.Log("TutorialUI1 Activated");

        // 8�� ��� �� TutorialUI1 ���� (2��~10�� ���� Ȱ��ȭ)
        yield return new WaitForSeconds(8f);
        TutorialUI1.SetActive(false);
        Debug.Log("TutorialUI1 Deactivated");

        // 10�� ��� �� TutorialUI2 ǥ�� (20�ʺ��� TutorialUI2 Ȱ��ȭ)
        yield return new WaitForSeconds(10f);
        TutorialUI2.SetActive(true);
        Debug.Log("TutorialUI2 Activated");
    }
}
