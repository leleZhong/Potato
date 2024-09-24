using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInteraction : MonoBehaviour
{
 
    public GameObject TutorialUI1; // ��ȣ�ۿ� UI
    public GameObject TutorialUI2;


    void Start()
    {
        TutorialUI1.SetActive(false); // ó���� UI�� ��Ȱ��ȭ
        TutorialUI2.SetActive(false);
        
    }



    void OnTriggerEnter(Collider other)
    {
        // �±׸� Ȯ���Ͽ� UI�� Ȱ��ȭ�մϴ�.
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



