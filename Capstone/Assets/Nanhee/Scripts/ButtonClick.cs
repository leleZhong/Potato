using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public GameObject interactionUI; // 상호작용 UI 오브젝트 참조
    Button button;
    private Animator animator;


    void Start()
    {
        //interactionUI.SetActive(false); // 초기에는 UI를 비활성화
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        
    }

    void OnTriggerEnter(Collider other)
    {

        //"" 태그를 가진 오브젝트에 접촉하면 UI를 활성화
        if (other.CompareTag("interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(true);
        }

    }

    // TODO : 오브젝트 상호작용 후
    public void onClickButton()
    {
        if (interactionUI.activeSelf && GameObject.FindWithTag("CorrectNumber"))
        {
            animator.SetBool("Open", true);
            interactionUI.SetActive(false); // UI를 비활성화
        }
        else
        {

        }
    }





    void OnTriggerExit(Collider other)
    {
        // 오브젝트가 범위를 벗어나면 UI를 비활성화
        if (other.CompareTag("interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(false);
        }
    }
}
