using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger: MonoBehaviour
{
    private Material mat;
    private Color[] colors = { Color.red, Color.blue, Color.yellow, Color.green };
    private int currentColorIndex = 0;

    // �� ���� ���� ����
    [SerializeField] private int redCount = 0;
    [SerializeField] private int blueCount = 0;
    [SerializeField] private int yellowCount = 0;
    [SerializeField] private int greenCount = 0;

    void Start()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
        InvokeRepeating("ChangeColor", 0f, 1f); // 1�ʸ��� ChangeColor �޼��带 ȣ��
    }

    void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        mat.color = colors[currentColorIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        // �ڽ��� ������ ������
        Color selfColor = mat.color;

        // �ڽ��� ����� ��ġ�ϴ� ������ �ø�
        if (selfColor == Color.red)
            redCount++;
        else if (selfColor == Color.blue)
            blueCount++;
        else if (selfColor == Color.yellow)
            yellowCount++;
        else if (selfColor == Color.green)
            greenCount++;

        // ����� ���� ���
        Debug.Log(selfColor + " ������ ������ ����Ǿ����ϴ�: red=" + redCount + ", blue=" + blueCount + ", yellow=" + yellowCount + ", green=" + greenCount);
    }
}


