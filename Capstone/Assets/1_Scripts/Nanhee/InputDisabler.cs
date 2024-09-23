using System.Collections;
using UnityEngine;

public class InputDisabler : MonoBehaviour
{
    private bool isInputBlocked = false; // �Է��� ���ܵǾ����� Ȯ���ϴ� ����
    public float blockDuration = 3.0f; // �Է��� ������ �ð� (��)

    void Update()
    {
        if (isInputBlocked)
        {
            // �Է� ���� ���� ��, Ű����� ���콺 �Է��� ������
            return;
        }

        // �Է� ó��
        HandleInput();
    }

    // �ܺο��� ȣ���� �� �ִ� �Է� ���� �Լ�
    public void BlockInput(float duration)
    {
        if (!isInputBlocked)
        {
            StartCoroutine(BlockInputForDuration(duration));
        }
    }

    // ���콺�� Ű���� �Է��� ó���ϴ� �Լ� (�ӽ� ����)
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W pressed");
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button pressed");
        }
        // ��Ÿ �Է� ó�� �ڵ� �߰� ����
    }

    // �Է� ������ ó���ϴ� �ڷ�ƾ
    private IEnumerator BlockInputForDuration(float duration)
    {
        isInputBlocked = true; // �Է� ���� ���·� ����

        yield return new WaitForSeconds(duration); // ������ �ð� ���� ���

        isInputBlocked = false; // �Է� ���� ����
    }
}
