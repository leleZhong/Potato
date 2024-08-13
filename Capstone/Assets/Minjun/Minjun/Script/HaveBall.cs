using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveBall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // 'ball' �±׸� ���� ������Ʈ�� �����ϸ� ����
        if (other.CompareTag("ball"))
        {
            // Ball ������Ʈ�� ĳ������ �ڽ����� �����ϰ� Ư�� ��ġ�� �̵�
            other.transform.SetParent(transform);
            other.transform.localPosition = new Vector3(0.4f, 1, 0.4f); // ���ϴ� ��ġ�� �̵�, ��: (0, 1, 0)
        }
    }
}
