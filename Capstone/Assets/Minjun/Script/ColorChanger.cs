using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorChanger : MonoBehaviour
{
    private Material mat;
    private Color[] colors = { Color.red, Color.blue, Color.yellow, Color.green };
    private int currentColorIndex = 0;
    private bool isPlayerInRange = false;
    private string currentTag = "";

    // �� ���� ���� ����
    [SerializeField] private int redCount = 0;
    [SerializeField] private int blueCount = 0;
    [SerializeField] private int yellowCount = 0;
    [SerializeField] private int greenCount = 0;

    void Start()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
        InvokeRepeating("ChangeColor", 0f, 2f); // 2�ʸ��� ChangeColor �޼��带 ȣ��
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            IncrementColorCount();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ResetCounts();
        }

        CheckCounts();
    }

    void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        mat.color = colors[currentColorIndex];
    }

    void IncrementColorCount()
    {
        // �ڽ��� ������ ������
        Color selfColor = mat.color;

        // �ڽ��� ����� ��ġ�ϴ� ������ �ø�
        if (currentTag == "red" && selfColor == Color.red)
            redCount++;
        else if (currentTag == "blue" && selfColor == Color.blue)
            blueCount++;
        else if (currentTag == "yellow" && selfColor == Color.yellow)
            yellowCount++;
        else if (currentTag == "green" && selfColor == Color.green)
            greenCount++;

        // ����� ���� ���
        Debug.Log(selfColor + " ������ ������ ����Ǿ����ϴ�: red=" + redCount + ", blue=" + blueCount + ", yellow=" + yellowCount + ", green=" + greenCount);
    }

    void ResetCounts()
    {
        redCount = 0;
        blueCount = 0;
        yellowCount = 0;
        greenCount = 0;

        // ���� �ʱ�ȭ �α� ���
        Debug.Log("������ �ʱ�ȭ�Ǿ����ϴ�: red=" + redCount + ", blue=" + blueCount + ", yellow=" + yellowCount + ", green=" + greenCount);
    }

    void CheckCounts()
    {
        if (redCount == 3 && blueCount == 4 && yellowCount == 4 && greenCount == 4)
        {
            // Scene2�� ��ȯ
            SceneManager.LoadScene("Jeongmin");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("red") || other.CompareTag("blue") || other.CompareTag("yellow") || other.CompareTag("green"))
        {
            isPlayerInRange = true;
            currentTag = other.tag;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(currentTag))
        {
            isPlayerInRange = false;
            currentTag = "";
        }
    }
}
