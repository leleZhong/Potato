using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityToggle : MonoBehaviour
{
    private Renderer colorBallRenderer; // Color Ball ������Ʈ�� Renderer

    void Start()
    {
        // ������ �� �ڽ� ������Ʈ �� 'Color Ball' �±׸� ���� ������Ʈ�� ã�� �������� ��Ȱ��ȭ�մϴ�.
        Transform colorBallTransform = transform.Find("color ball"); // 'Color Ball'�� �� ������Ʈ�� �ڽ����� �����Ǿ� �־�� �մϴ�.
        if (colorBallTransform != null)
        {
            colorBallRenderer = colorBallTransform.GetComponent<Renderer>();
            if (colorBallRenderer != null)
                colorBallRenderer.enabled = false; // �ʱ⿡�� Color Ball�� ������ �ʰ� ����
        }
        else
        {
            Debug.LogError("No child with tag 'Color Ball' found in the object hierarchy. Check the structure.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 'Ball' �±׸� ���� ������Ʈ�� �����ϸ� ����
        if (other.CompareTag("ball"))
        {
            Renderer ballRenderer = other.GetComponent<Renderer>();
            if (ballRenderer != null)
                ballRenderer.enabled = false; // Ball ������Ʈ�� ������ �ʰ� ����

            if (colorBallRenderer != null)
                colorBallRenderer.enabled = true; // Color Ball ������Ʈ�� ���̰� ����
        }
    }
}
