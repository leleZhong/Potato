using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed;

    void Update()
    {
        float h = GameManager.Instance._isAction ? 0 : Input.GetAxisRaw("Horizontal");
        float v = GameManager.Instance._isAction ? 0 : Input.GetAxisRaw("Vertical");
        Vector3 nextPos = new Vector3(h, 0, v) * _speed * Time.deltaTime;
        Vector3 currentPos = transform.position + nextPos;

        transform.position = currentPos;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedObject = hit.transform.gameObject;
                ObjData objData = clickedObject.GetComponent<ObjData>();
                
                if (objData != null) // ObjData 컴포넌트가 있는 경우만 Action을 실행
                {
                    GameManager.Instance.Action(clickedObject);
                }
            }
        }
    }
}
