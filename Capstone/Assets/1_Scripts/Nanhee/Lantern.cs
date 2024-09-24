using UnityEngine;

public class Lantern : MonoBehaviour
{
    public GameObject player;        // 캐릭터 오브젝트
    public Transform handTransform;  // 캐릭터 손의 위치
    private GameObject lantern;      // 상호작용할 랜턴 오브젝트
    private bool isNearLantern = false;
    private bool isHoldingLantern = false;

    void Update()
    {
        // 'E' 키를 눌렀을 때
        if (isNearLantern && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHoldingLantern)
            {
                // 랜턴을 손에 들기
                PickUpLantern();
            }
            else
            {
                // 랜턴을 내려놓기
                DropLantern();
            }
        }
    }

    private void PickUpLantern()
    {
        // 랜턴을 손 위치로 이동시키고 부모를 캐릭터 손으로 설정
        lantern.transform.position = handTransform.position;
        lantern.transform.rotation = handTransform.rotation;
        lantern.transform.parent = handTransform;

        lantern.transform.Rotate(270f, 0f, 0f);

        // 상호작용 상태 변경
        isHoldingLantern = true;
    }

    private void DropLantern()
    {
        // 랜턴의 부모를 해제하여 원래 상태로 되돌림
        lantern.transform.parent = null;

        // 랜턴을 놓았을 때 원하는 위치로 이동시키고 싶다면 위치를 설정하세요
        // 예를 들어 lantern.transform.position = new Vector3(x, y, z);

        // 상호작용 상태 변경
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
