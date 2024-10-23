using System.Collections;
using UnityEngine;

public class CanvasDelayActivator : MonoBehaviour
{
    public GameObject canvas;  // ĵ������ ������ ����

    void Start()
    {
        // ó���� ĵ������ ��Ȱ��ȭ
        canvas.SetActive(false);

        // 10�� �Ŀ� ĵ������ Ȱ��ȭ��Ű�� �ڷ�ƾ ����
        StartCoroutine(ActivateCanvasAfterDelay(26f));
       
       

    }

    // ������ �� ĵ������ Ȱ��ȭ��Ű�� �ڷ�ƾ
    IEnumerator ActivateCanvasAfterDelay(float delay)
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        // ĵ���� Ȱ��ȭ
        canvas.SetActive(true);
    }
}
