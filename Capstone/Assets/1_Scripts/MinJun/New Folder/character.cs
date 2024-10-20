using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]       //����Ƽ ���ο��� Ȯ��
    public Transform characterBody; // �ش�Ǵ� ��ü�� �巡�� �� �����ϸ� �°� ���
    public Camera playerCamera; // ī�޶� ������Ʈ�� �ν����Ϳ��� ����

    public float applySpeed; // ����Ǵ� �ӵ��� ������ ����

    public PhotonView _pv;
    Transform _tf;

    bool isRun;       // �޸��� ���� Ȯ�� ����
    bool jump;       // ���� ���� Ȯ�� ����
    bool isJump;     // �������� ���� �����ϰ� �ϴ� ����

    bool fDown;       // fŰ�� �������� ��ȣ�ۿ��ϴ� ����

    bool attack;       // ���� ���� Ȯ�� ����
    bool isattack;     // ���� �����ϰ� �ϴ� ����

    float hAxis;     // Ű�� �ޱ����� ����
    float vAxis;     // Ű�� �ޱ����� ����

    float SprintSpeed = 10.6f;                          // �⺻ �޸��� �ӵ�
    float MoveSpeed = 4.0f;                            // �⺻ �ȴ� �ӵ�
    float jumppower = 10;                              // ��������

    bool isBorder;    // ������ ���� ����    

    Vector3 moveVec;        // ���� ������ ���� ����
    Vector3 dodgeVec;

    Rigidbody rigid;       // ����ȿ�� ����
    Animator anim;     // �ִϸ��̼� �ֱ� ���� �Լ�

    public float mouseSensitivity = 100f; // ���콺 �ΰ���
    private float xRotation = 0f; // ī�޶� ���� ȸ�� ������ ������ ����
    private float yRotation = 0f; // ī�޶� ���� ȸ�� ������ ������ ����

    // �߰��� �κ�: �Ҹ� ������ ����
    public AudioSource audioSource;  // �Ҹ��� ����� ����� �ҽ�
    public AudioClip jumpSound;  // ���� ����
    public AudioClip walkSound;  // �ȴ� �Ҹ�

    public float walkVolume = 0.3f; // �ȴ� �Ҹ� ũ�� ���� ����
    public float runVolume = 1.0f;  // �ٴ� �Ҹ� ũ�� ���� ����
    public float jumpVolume = 0.7f;  // ���� �Ҹ� ũ�� ���� ���� �߰�

    void Start()
    {

        // AudioSource �Ҵ�
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
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���� ���� ����

        yRotation += mouseX;

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // ī�޶� ���� ȸ��
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f); // ĳ���� �¿� ȸ��
    }

    public void Move()       // ĳ���� ������ ����
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        bool isMoving = moveVec.magnitude > 0;
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);

        // �ٱ�
        if (isShiftPressed && isMoving && !isJump) // ���� ���� �ƴ� ���� ����
        {
            isRun = true;
            applySpeed = SprintSpeed;
            PlayFootstepSound(runVolume, 1.5f);
        }
        // �ȱ�
        else if (!isShiftPressed && isMoving && !isJump) // ���� ���� �ƴ� ���� ����
        {
            isRun = false;
            applySpeed = MoveSpeed;
            PlayFootstepSound(walkVolume, 1.0f);
        }

        // ���� �� �Ҹ� ����
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

            characterBody.forward = lookForward;  // ĳ������ ���� ����
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
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, pitch, Time.deltaTime * 5f);  // �Ҹ� �ӵ� ��ȭ
        }
    }

    public void Jump()
    {
        jump = Input.GetButtonDown("Jump");
        if (jump && !isJump)
        {
            // ���� ���� ���
            if (audioSource != null && jumpSound != null)
            {
                audioSource.Stop(); // ���� �Ҹ� ����� ���� ���� �Ҹ� ����
                audioSource.clip = jumpSound;   // ���� ���带 AudioSource�� ����
                audioSource.volume = jumpVolume; // ���� ���� ����
                audioSource.loop = false;        // ���� �Ҹ��� �� ���� ����ǵ��� ���� ��Ȱ��ȭ
                audioSource.Play();              // ���� �Ҹ� ���
            }

            rigid.AddForce(Vector3.up * jumppower, ForceMode.Impulse);  // �������� ���� ���ϴ� �Լ� �̿�
            isJump = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;

            // ���� �� �ȱ�/�ٱ� �Ҹ� �ٽ� ���
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
