using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveBall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // 'ball' 태그를 가진 오브젝트에 접촉하면 실행
        if (other.CompareTag("ball"))
        {
            // Ball 오브젝트를 캐릭터의 자식으로 설정하고 특정 위치로 이동
            other.transform.SetParent(transform);
            other.transform.localPosition = new Vector3(0.4f, 1, 0.4f); // 원하는 위치로 이동, 예: (0, 1, 0)
        }
    }
}
