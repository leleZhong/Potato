using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TutorialCharacter : MonoBehaviour
{
    [SerializeField]       //유니티 내부에서 확인
    public Transform characterBody; // 해당되는 개체를 드래그 후 지정하면 맞게 사용
    public Camera playerCamera; // 카메라 오브젝트를 인스펙터에서 지정

    public float applySpeed; // 적용되는 속도를 변수로 만듦

    public PhotonView _pv;
    Transform _tf;

    bool isRun;       //달리기 여부 확인 변수
    bool jump;       // 점프 여부 확인 변수
    bool isJump;     // 땅에서만 점프 가능하게 하는 변수
    bool isInputEnabled = false; // 입력 가능 여부 제어 변수

    bool fDown;       // f키를 눌렀을때 상호작용하는 변수

    bool attack;       // 공격 여부 확인 변수
    bool isattack;     // 공격 가능하게 하는 변수

    float hAxis;     //키값 받기위한 변수
    float vAxis;     //키값 받기위한 변수

    float SprintSpeed = 10.6f;                          // 기본 달리는 속도
    float MoveSpeed = 4.0f;                            // 기본 걷는 속도
    float jumppower = 10;                              // 점프세기

    bool isBorder;    //벽관통 막는 변수    

    Vector3 moveVec;        //조건 설정을위한 백터
    Vector3 dodgeVec;

    Rigidbody rigid;       // 물리효과 구현
    Animator anim;     //애니메이션 넣기 위한 함수

    public float mouseSensitivity = 100f; // 마우스 민감도
    private float xRotation = 0f; // 카메라 상하 회전 각도를 저장할 변수
    private float yRotation = 0f; // 카메라 상하 회전 각도를 저장할 변수

    void Start()
    {
        _tf = GetComponent<Transform>();

        if (Camera.main == null)
        {
            // Debug.LogError("Main Camera is not found!");
        }
        else
        {
            var cameraController = Camera.main.GetComponent<CameraController>();
            if (cameraController != null)
            {
                Transform cameraTransform = _tf.Find("Camera1");
                if (cameraTransform != null)
                {
                    cameraController._target = cameraTransform;
                }
                else
                {
                    Debug.LogError("Camera1 object not found as a child of this transform.");
                }
            }
            else
            {
                
            }
        }

        if (playerCamera != null)
        {
            // playerCamera.gameObject.AddComponent<AudioListener>();
        }
        else
        {
            Debug.LogError("Player camera is not assigned in the inspector.");
        }

        rigid = GetComponent<Rigidbody>();
        if (rigid == null)
        {
            Debug.LogError("Rigidbody component not found!");
        }

        

        applySpeed = 2.0f;

        StartCoroutine(DisableInputForSeconds(19)); // 17초간 입력 차단

        
    }

    IEnumerator DisableInputForSeconds(float seconds)
    {
        isInputEnabled = false;
        yield return new WaitForSeconds(seconds);
        isInputEnabled = true; // 5초 후 입력 활성화
    }

    void Update()
    {
        if (isInputEnabled)
        {
            Aim();
            Move();
            Jump();
        }
    }

    public void SetCharacter(Transform newCharacterBody)
    {
        characterBody = newCharacterBody;
        anim = characterBody.GetComponent<Animator>();

        if (anim == null)
        {
            Debug.LogError("Animator component not found on the characterBody!");
        }
    }


    public void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical'");
    }

    public void Aim()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 상하 각도 제한

        yRotation += mouseX;

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // 카메라 상하 회전
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f); // 캐릭터 좌우 회전
    }

    public void Move()       // 캐릭터 움직임 구현
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (Input.GetKey(KeyCode.LeftShift))  // 좌측 shift를 누르면 값을 받아오는 함수이용
        {
            isRun = true;
            applySpeed = SprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) // 좌측 shift를 떼면 값을 받아오는 함수이용
        {
            isRun = false;
            applySpeed = MoveSpeed;
        }

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        anim.SetBool("isRun", isRun);
        anim.SetBool("isWalk", isMove);

        // 벽 충돌 검사 로직
        if (isMove)
        {
            Vector3 lookForward = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z).normalized;
            Vector3 lookRight = new Vector3(playerCamera.transform.right.x, 0, playerCamera.transform.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;  // 캐릭터의 방향 설정

            // 앞으로 이동할 때 벽이 있으면 속도를 0으로 설정
            if (Physics.Raycast(transform.position, lookForward, 10, LayerMask.GetMask("Wall")))
            {
                applySpeed = 0;  // 벽이 있으면 속도를 0으로
            }

            // 이동 실행
            transform.position += moveDir * Time.deltaTime * applySpeed;
            Debug.DrawRay(transform.position, lookForward * 10, Color.green);  // 디버그 레이 표시
        }

    }

    public void Jump()
    {
        jump = Input.GetButtonDown("Jump");
        if (jump && !isJump)
        {

            rigid.AddForce(Vector3.up * jumppower, ForceMode.Impulse);  //물리적인 힘을 가하는 함수 이용
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isJump = false;
    //        anim.SetBool("isJump", false);
    //    }
    //}
}
