using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject selectionPanel;
    public Button character1Button;
    public Button character2Button;

    public GameObject character1;

    private Vector3 spawnPoint1 = new Vector3(-90, 0, -39);
    private Vector3 spawnPoint2 = new Vector3(180, 0, -39);

    private void Start()
    {
        character1Button.onClick.AddListener(() => SelectCharacter(1));
        character2Button.onClick.AddListener(() => SelectCharacter(2));
    }

    public void SelectCharacter(int characterIndex)
    {
        selectionPanel.SetActive(false); // 선택 후 패널을 비활성화합니다.
        Vector3 spawnPosition = characterIndex == 1 ? spawnPoint1 : spawnPoint2;
        MoveCharacter(spawnPosition);
    }

    private void MoveCharacter(Vector3 newPosition)
    {
        Debug.Log("MoveCharacter called. New Position: " + newPosition);
        character1.transform.position = newPosition;
    }
}
