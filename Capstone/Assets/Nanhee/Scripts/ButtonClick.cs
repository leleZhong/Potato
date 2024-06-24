using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public GameObject interactionUI; // ��ȣ�ۿ� UI ������Ʈ ����
    Button button;
    private Animator animator;


    void Start()
    {
        //interactionUI.SetActive(false); // �ʱ⿡�� UI�� ��Ȱ��ȭ
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        
    }

    void OnTriggerEnter(Collider other)
    {

        //"" �±׸� ���� ������Ʈ�� �����ϸ� UI�� Ȱ��ȭ
        if (other.CompareTag("interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(true);
        }

    }

    // TODO : ������Ʈ ��ȣ�ۿ� ��
    public void onClickButton()
    {
        if (interactionUI.activeSelf && GameObject.FindWithTag("CorrectNumber"))
        {
            animator.SetBool("Open", true);
            interactionUI.SetActive(false); // UI�� ��Ȱ��ȭ
        }
        else
        {

        }
    }





    void OnTriggerExit(Collider other)
    {
        // ������Ʈ�� ������ ����� UI�� ��Ȱ��ȭ
        if (other.CompareTag("interaction") || other.CompareTag("CorrectNumber"))
        {
            interactionUI.SetActive(false);
        }
    }
}
