using UnityEngine;

public class Lantern : MonoBehaviour
{
    public GameObject player;        // ĳ���� ������Ʈ
    public Transform handTransform;  // ĳ���� ���� ��ġ
    private GameObject lantern;      // ��ȣ�ۿ��� ���� ������Ʈ
    private bool isNearLantern = false;
    private bool isHoldingLantern = false;

    void Update()
    {
        // 'E' Ű�� ������ ��
        if (isNearLantern && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHoldingLantern)
            {
                // ������ �տ� ���
                PickUpLantern();
            }
            else
            {
                // ������ ��������
                DropLantern();
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

    private void DropLantern()
    {
        // ������ �θ� �����Ͽ� ���� ���·� �ǵ���
        lantern.transform.parent = null;

        // ������ ������ �� ���ϴ� ��ġ�� �̵���Ű�� �ʹٸ� ��ġ�� �����ϼ���
        // ���� ��� lantern.transform.position = new Vector3(x, y, z);

        // ��ȣ�ۿ� ���� ����
        isHoldingLantern = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lantern"))
        {
            isNearLantern = true;
            lantern = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lantern"))
        {
            isNearLantern = false;
            lantern = null;
        }
    }
}
