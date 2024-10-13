using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending : MonoBehaviour
{

    void Start()
    {
       

        // 5�� �Ŀ� �߷� �ۿ� �� ���� ���� �ڷ�ƾ ����
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
                renderer.material.color = new Color(Random.value, Random.value, Random.value); // ���� ���� ����
            }
        }
    }

}
