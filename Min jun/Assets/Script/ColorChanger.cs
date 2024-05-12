using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger: MonoBehaviour
{
    private Material mat;
    private Color[] colors = { Color.red, Color.blue, Color.yellow, Color.green };
    private int currentColorIndex = 0;

    // 각 색상에 대한 변수
    [SerializeField] private int redCount = 0;
    [SerializeField] private int blueCount = 0;
    [SerializeField] private int yellowCount = 0;
    [SerializeField] private int greenCount = 0;

    void Start()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
        InvokeRepeating("ChangeColor", 0f, 1f); // 1초마다 ChangeColor 메서드를 호출
    }

    void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        mat.color = colors[currentColorIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        // 자신의 색상을 가져옴
        Color selfColor = mat.color;

        // 자신의 색상과 일치하는 변수를 올림
        if (selfColor == Color.red)
            redCount++;
        else if (selfColor == Color.blue)
            blueCount++;
        else if (selfColor == Color.yellow)
            yellowCount++;
        else if (selfColor == Color.green)
            greenCount++;

        // 변경된 변수 출력
        Debug.Log(selfColor + " 색상의 변수가 변경되었습니다: red=" + redCount + ", blue=" + blueCount + ", yellow=" + yellowCount + ", green=" + greenCount);
    }
}


