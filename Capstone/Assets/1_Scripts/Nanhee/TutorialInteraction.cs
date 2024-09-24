using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInteraction : MonoBehaviour
{
 
    public GameObject TutorialUI1; // 상호작용 UI
    public GameObject TutorialUI2;


    void Start()
    {
        TutorialUI1.SetActive(false); // 처음에 UI를 비활성화
        TutorialUI2.SetActive(false);
        
    }



    void OnTriggerEnter(Collider other)
    {
        // 태그를 확인하여 UI를 활성화합니다.
        if (other.CompareTag("Trigger1"))
        {
            TutorialUI1.SetActive(true);
        }
        else if (other.CompareTag("Trigger2"))
        {
            TutorialUI2.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Trigger1"))
        {
            TutorialUI1.SetActive(false);
        }
        else if (other.CompareTag("Trigger2"))
        {
            TutorialUI2.SetActive(false);
        }
    }

    
    }



