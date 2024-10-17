using UnityEngine;

public class EndingBook : MonoBehaviour
{
    public GameObject player;        // ĳ���� ������Ʈ
    public Transform handTransform;  // ĳ���� ���� ��ġ
    private GameObject lantern;      // ��ȣ�ۿ��� å ������Ʈ
    private bool isNearLantern = false;
    private bool isHoldingLantern = false;

    void Update()
    {
        // ���������� å �鵵�� ����
        if (isNearLantern && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHoldingLantern)
            {
                // ������ �տ� ���
                PickUpLantern();
            }
            else
            {
                
            }
        }
    }

    private void PickUpLantern()
    {
        // ������ �� ��ġ�� �̵���Ű�� �θ� ĳ���� ������ ����
        lantern.transform.position = handTransform.position;
        lantern.transform.rotation = handTransform.rotation;
        lantern.transform.parent = handTransform;

        lantern.transform.Rotate(270f, 0f, 0f);

        // ��ȣ�ۿ� ���� ����
        isHoldingLantern = true;
    }


    
}
