using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityToggle : MonoBehaviour
{
    private Renderer colorBallRenderer; // Color Ball 오브젝트의 Renderer

    void Start()
    {
        // 시작할 때 자식 오브젝트 중 'Color Ball' 태그를 가진 오브젝트를 찾고 렌더러를 비활성화합니다.
        Transform colorBallTransform = transform.Find("color ball"); // 'Color Ball'은 이 오브젝트의 자식으로 설정되어 있어야 합니다.
        if (colorBallTransform != null)
        {
            colorBallRenderer = colorBallTransform.GetComponent<Renderer>();
            if (colorBallRenderer != null)
                colorBallRenderer.enabled = false; // 초기에는 Color Ball을 보이지 않게 설정
        }
        else
        {
            Debug.LogError("No child with tag 'Color Ball' found in the object hierarchy. Check the structure.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 'Ball' 태그를 가진 오브젝트에 접촉하면 실행
        if (other.CompareTag("ball"))
        {
            Renderer ballRenderer = other.GetComponent<Renderer>();
            if (ballRenderer != null)
                ballRenderer.enabled = false; // Ball 오브젝트를 보이지 않게 설정

            if (colorBallRenderer != null)
                colorBallRenderer.enabled = true; // Color Ball 오브젝트를 보이게 설정
        }
    }
}
