using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending : MonoBehaviour
{

    void Start()
    {
       

        // 5초 후에 중력 작용 및 색상 변경 코루틴 시작
        StartCoroutine(ActivateGravityAndChangeColor());
    }

    
    IEnumerator ActivateGravityAndChangeColor()
    {
        yield return new WaitForSeconds(5f);

        foreach (Transform child in transform)
        {
            
            Renderer renderer = child.GetComponent<Renderer>();

            

            if (renderer != null)
            {
                renderer.material.color = new Color(Random.value, Random.value, Random.value); // 랜덤 색상 변경
            }
        }
    }

}
