using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending : MonoBehaviour
{

    void Start()
    {
        // 처음 5초 동안 자식 오브젝트의 중력을 비활성화
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
            }
        }

        // 5초 후에 중력 작용 및 색상 변경 코루틴 시작
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
                rb.useGravity = true; // 5초 후에 중력 작용 시작
            }

            if (renderer != null)
            {
                renderer.material.color = new Color(Random.value, Random.value, Random.value); // 랜덤 색상 변경
            }
        }
    }

}
