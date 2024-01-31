using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigid;

    [SerializeField] float _speed = 10.0f;
    [SerializeField] float _jumpForce = 500.0f;
    bool isJumping = false;
    // float _maxSpeed = 20.0f;

    void Start()
    {
        Application.targetFrameRate = 60;   // 60FPS로 고정

        _rigid = GetComponent<Rigidbody>();

        // KeyAction이 호출되면 구독할 함수
        Managers.Input.KeyAction -= OnKeyboard; // 다른 곳에서 이미 호출되었을 가능성이 있으므로 삭제
        Managers.Input.KeyAction += OnKeyboard;
    }

    void Update()
    {

    }

    void OnKeyboard()
    {
        // float speedX = Mathf.Abs(_rigid.velocity.x);
        // float speedZ = Mathf.Abs(_rigid.velocity.z);
        // 속도 제한
        // if ((speedX < _maxSpeed) && (speedZ < _maxSpeed))
        {
            // WASD 입력에 따른 움직임
            if (Input.GetKey(KeyCode.W))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
                transform.position += Vector3.forward * Time.deltaTime * _speed;
                // this._rigid.AddForce(Vector3.forward * _speed);
            }
    
            if (Input.GetKey(KeyCode.S))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
                transform.position += Vector3.back * Time.deltaTime * _speed;
                // this._rigid.AddForce(Vector3.back * _speed);
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
                transform.position += Vector3.left * Time.deltaTime * _speed;
                // this._rigid.AddForce(Vector3.left * _speed);
            }
    
            if (Input.GetKey(KeyCode.D))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
                transform.position += Vector3.right * Time.deltaTime * _speed;
                // this._rigid.AddForce(Vector3.right * _speed);
            }
        }

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            _rigid.AddForce(Vector3.up * _jumpForce);
            isJumping = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isJumping = false;
    }
}
