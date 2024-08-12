using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending : MonoBehaviour
{

    void Start()
    {
        // ó�� 5�� ���� �ڽ� ������Ʈ�� �߷��� ��Ȱ��ȭ
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
            }
        }

        // 5�� �Ŀ� �߷� �ۿ� �� ���� ���� �ڷ�ƾ ����
        StartCoroutine(ActivateGravityAndChangeColor());
    }

    
    IEnumerator ActivateGravityAndChangeColor()
    {
        yield return new WaitForSeconds(5f);

        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            Renderer renderer = child.GetComponent<Renderer>();

            if (rb != null)
            {
                rb.useGravity = true; // 5�� �Ŀ� �߷� �ۿ� ����
            }

            if (renderer != null)
            {
                renderer.material.color = new Color(Random.value, Random.value, Random.value); // ���� ���� ����
            }
        }
    }

}
