using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]       //유니티 내부에서 확인
    public Transform characterBody; // 해당되는 개체를 드래그 후 지정하면 맞게 사용
    public Camera playerCamera; // 카메라 오브젝트를 인스펙터에서 지정

    public float applySpeed; // 적용되는 속도를 변수로 만듦

    public PhotonView _pv;
    Transform _tf;

    bool isRun;       // 달리기 여부 확인 변수
    bool jump;       // 점프 여부 확인 변수
    bool isJump;     // 땅에서만 점프 가능하게 하는 변수

    bool fDown;       // f키를 눌렀을때 상호작용하는 변수

    bool attack;       // 공격 여부 확인 변수
    bool isattack;     // 공격 가능하게 하는 변수

    float hAxis;     // 키값 받기위한 변수
    float vAxis;     // 키값 받기위한 변수

    float SprintSpeed = 10.6f;                          // 기본 달리는 속도
    float MoveSpeed = 4.0f;                            // 기본 걷는 속도
    float jumppower = 10;                              // 점프세기

    bool isBorder;    // 벽관통 막는 변수    

    Vector3 moveVec;        // 조건 설정을 위한 벡터
    Vector3 dodgeVec;

    Rigidbody rigid;       // 물리효과 구현
    Animator anim;     // 애니메이션 넣기 위한 함수

    public float mouseSensitivity = 100f; // 마우스 민감도
    private float xRotation = 0f; // 카메라 상하 회전 각도를 저장할 변수
    private float yRotation = 0f; // 카메라 상하 회전 각도를 저장할 변수

    // 추가된 부분: 소리 변수들 선언
    public AudioSource audioSource;  // 소리를 재생할 오디오 소스
    public AudioClip jumpSound;  // 점프 사운드
    public AudioClip walkSound;  // 걷는 소리

    public float walkVolume = 0.3f; // 걷는 소리 크기 조절 변수
    public float runVolume = 1.0f;  // 뛰는 소리 크기 조절 변수
    public float jumpVolume = 0.7f;  // 점프 소리 크기 조절 변수 추가

    void Start()
    {

        // AudioSource 할당
        audioSource = FindAnyObjectByType<AudioSource>();
        _tf = GetComponent<Transform>();

        // if (Camera.main == null)
        // {
        //     Debug.LogError("Main Camera is not found!");
        // }
        // else
        // {
        //     var cameraController = Camera.main.GetComponent<CameraController>();
        //     if (cameraController != null)
        //     {
        //         Transform cameraTransform = _tf.Find("Camera1");
        //         if (cameraTransform != null)
        //         {
        //             cameraController._target = cameraTransform;
        //         }
        //     }
        //     else
        //     {
        //         Debug.LogError("CameraController component not found on Main Camera!");
        //     }
        // }

        if (playerCamera != null)
        {
            playerCamera.gameObject.AddComponent<AudioListener>();
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

        anim = characterBody.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator component not found on characterBody!");
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found!");
        }

        applySpeed = 2.0f;
    }

    void Update()
    {
        if (_pv.IsMine)
        {
            Aim();
            Move();
            Jump();
        }
    }

    public void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
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
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        bool isMoving = moveVec.magnitude > 0;
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);

        // 뛰기
        if (isShiftPressed && isMoving && !isJump) // 점프 중이 아닐 때만 실행
        {
            isRun = true;
            applySpeed = SprintSpeed;
            PlayFootstepSound(runVolume, 1.5f);
        }
        // 걷기
        else if (!isShiftPressed && isMoving && !isJump) // 점프 중이 아닐 때만 실행
        {
            isRun = false;
            applySpeed = MoveSpeed;
            PlayFootstepSound(walkVolume, 1.0f);
        }

        // 멈출 때 소리 중지
        if ((!isMoving || isJump) && audioSource.isPlaying && audioSource.clip == walkSound)
        {
            audioSource.Stop();
        }

        anim.SetBool("isRun", isRun);
        anim.SetBool("isWalk", isMoving);

        if (isMoving)
        {
            Vector3 lookForward = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z).normalized;
            Vector3 lookRight = new Vector3(playerCamera.transform.right.x, 0, playerCamera.transform.right.z).normalized;
            Vector3 moveDir = lookForward * moveVec.z + lookRight * moveVec.x;

            characterBody.forward = lookForward;  // 캐릭터의 방향 설정
            transform.position += moveDir * Time.deltaTime * applySpeed;
        }
    }

    private void PlayFootstepSound(float volume, float pitch)
    {
        if (!audioSource.isPlaying || audioSource.clip != walkSound)
        {
            audioSource.clip = walkSound;
            audioSource.volume = volume;
            audioSource.loop = true;
            audioSource.Play();
        }

        if (audioSource.isPlaying && audioSource.pitch != pitch)
        {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, pitch, Time.deltaTime * 5f);  // 소리 속도 변화
        }
    }

    public void Jump()
    {
        jump = Input.GetButtonDown("Jump");
        if (jump && !isJump)
        {
            // 점프 사운드 재생
            if (audioSource != null && jumpSound != null)
            {
                audioSource.Stop(); // 점프 소리 재생을 위해 기존 소리 중지
                audioSource.clip = jumpSound;   // 점프 사운드를 AudioSource에 설정
                audioSource.volume = jumpVolume; // 점프 볼륨 설정
                audioSource.loop = false;        // 점프 소리는 한 번만 재생되도록 루프 비활성화
                audioSource.Play();              // 점프 소리 재생
            }

            rigid.AddForce(Vector3.up * jumppower, ForceMode.Impulse);  // 물리적인 힘을 가하는 함수 이용
            isJump = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;

            // 착지 후 걷기/뛰기 소리 다시 재생
            if (moveVec.magnitude > 0)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    PlayFootstepSound(runVolume, 1.5f);
                }
                else
                {
                    PlayFootstepSound(walkVolume, 1.0f);
                }
            }
        }
    }
}
