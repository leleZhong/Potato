using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _speed;
    public GameManager _manager;

    void Update()
    {
        float h = _manager._isAction ? 0 : Input.GetAxisRaw("Horizontal");
        float v = _manager._isAction ? 0 : Input.GetAxisRaw("Vertical");
        Vector3 nextPos = new Vector3(h, 0, v) * _speed * Time.deltaTime;
        Vector3 currentPos = transform.position + nextPos;

        transform.position = currentPos;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

        }
    }
}
