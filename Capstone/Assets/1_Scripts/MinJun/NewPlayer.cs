using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    [SerializeField]
    public Transform characterBody;  // 캐릭터의 본체 Transform
    public Transform cameraArm;  // 카메라 암 Transform
    public float moveSpeed = 5f;  // 일반 이동 속도
    public float sprintSpeed = 10f;  // 달리기 속도
    public float jumpPower = 7f;  // 점프 힘
    public float applySpeed;  // 적용된 이동 속도

    bool isRun;  // 달리는 상태
    bool jump;  // 점프 입력 상태
    bool isJump;  // 점프 중 상태

    float hAxis;  // 수평 입력 축
    float vAxis;  // 수직 입력 축

    bool isBorder;  // 경계 충돌 상태

    Vector3 moveVec;  // 이동 벡터
    Rigidbody rigid;  // 리지드바디 컴포넌트
    Animator anim;  // 애니메이터 컴포넌트

    void Start()
    {
        // 리지드바디 및 애니메이터 컴포넌트 초기화
        rigid = GetComponent<Rigidbody>();
        anim = characterBody.GetComponent<Animator>();
        applySpeed = moveSpeed;
    }

    void Update()
    {
        // 매 프레임마다 입력, 에임, 이동, 점프 기능 호출
        GetInput();
        Aim();
        Move();
        Jump();
    }

    void GetInput()
    {
        // 플레이어 입력 처리
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }

    void Aim()
    {
        // 마우스 움직임에 따른 카메라 조작
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    void Move()
    {
        // 이동 벡터 계산 및 애니메이션 설정
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        bool isMove = moveVec.magnitude != 0;

        if (Input.GetKey(KeyCode.LeftShift) && isMove)
        {
            isRun = true;
            applySpeed = sprintSpeed;
            anim.SetBool("isRunning", true);  // isRunning 파라미터 설정
        }
        else
        {
            isRun = false;
            applySpeed = moveSpeed;
            anim.SetBool("isRunning", false);  // isRunning 파라미터 설정
        }

        anim.SetBool("isWalking", isMove);  // isWalking 파라미터 설정

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveVec.z + lookRight * moveVec.x;

            characterBody.forward = lookForward;
            if (!isBorder)
                transform.position += moveDir * Time.deltaTime * applySpeed;
            Debug.DrawRay(transform.position, lookForward, Color.green);
            isBorder = Physics.Raycast(transform.position, lookForward, 1, LayerMask.GetMask("Wall"));
        }
    }

    void Jump()
    {
        // 점프 입력 처리
        jump = Input.GetButtonDown("Jump");
        if (jump && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJump = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 착지 시 점프 상태 초기화
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }
    }
}