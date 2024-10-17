using UnityEngine;

public class EndingBook : MonoBehaviour
{
    public GameObject player;        // 캐릭터 오브젝트
    public Transform handTransform;  // 캐릭터 손의 위치
    private GameObject lantern;      // 상호작용할 책 오브젝트
    private bool isNearLantern = false;
    private bool isHoldingLantern = false;

    void Update()
    {
        // 몇초지나면 책 들도록 설정
        if (isNearLantern && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHoldingLantern)
            {
                // 랜턴을 손에 들기
                PickUpLantern();
            }
            else
            {
                
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


    
}
