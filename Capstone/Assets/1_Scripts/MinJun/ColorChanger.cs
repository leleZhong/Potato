using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Material mat;
    private Color[] colors = { Color.red, Color.blue, Color.yellow, Color.green };
    private int currentColorIndex = 0;

    void Start()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
        InvokeRepeating("ChangeColor", 0f, 2f); // 2초마다 ChangeColor 메서드를 호출
    }

    void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        mat.color = colors[currentColorIndex];
    }

    public Color GetCurrentColor()
    {
        return mat.color;
    }
}
