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

    // 각 색상에 대한 변수
    [SerializeField] private int redCount = 0;
    [SerializeField] private int blueCount = 0;
    [SerializeField] private int yellowCount = 0;
    [SerializeField] private int greenCount = 0;

    void Start()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
        InvokeRepeating("ChangeColor", 0f, 2f); // 2초마다 ChangeColor 메서드를 호출
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
        // 자신의 색상을 가져옴
        Color selfColor = mat.color;

        // 자신의 색상과 일치하는 변수를 올림
        if (currentTag == "red" && selfColor == Color.red)
            redCount++;
        else if (currentTag == "blue" && selfColor == Color.blue)
            blueCount++;
        else if (currentTag == "yellow" && selfColor == Color.yellow)
            yellowCount++;
        else if (currentTag == "green" && selfColor == Color.green)
            greenCount++;

        // 변경된 변수 출력
        Debug.Log(selfColor + " 색상의 변수가 변경되었습니다: red=" + redCount + ", blue=" + blueCount + ", yellow=" + yellowCount + ", green=" + greenCount);
    }

    void ResetCounts()
    {
        redCount = 0;
        blueCount = 0;
        yellowCount = 0;
        greenCount = 0;

        // 변수 초기화 로그 출력
        Debug.Log("변수가 초기화되었습니다: red=" + redCount + ", blue=" + blueCount + ", yellow=" + yellowCount + ", green=" + greenCount);
    }

    void CheckCounts()
    {
        if (redCount == 3 && blueCount == 4 && yellowCount == 4 && greenCount == 4)
        {
            // Scene2로 전환
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
