using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TutorialCharacter : MonoBehaviour
{
    [SerializeField]       //����Ƽ ���ο��� Ȯ��
    public Transform characterBody; // �ش�Ǵ� ��ü�� �巡�� �� �����ϸ� �°� ���
    public Camera playerCamera; // ī�޶� ������Ʈ�� �ν����Ϳ��� ����

    public float applySpeed; // ����Ǵ� �ӵ��� ������ ����

    public PhotonView _pv;
    Transform _tf;

    bool isRun;       //�޸��� ���� Ȯ�� ����
    bool jump;       // ���� ���� Ȯ�� ����
    bool isJump;     // �������� ���� �����ϰ� �ϴ� ����
    bool isInputEnabled = false; // �Է� ���� ���� ���� ����

    bool fDown;       // fŰ�� �������� ��ȣ�ۿ��ϴ� ����

    bool attack;       // ���� ���� Ȯ�� ����
    bool isattack;     // ���� �����ϰ� �ϴ� ����

    float hAxis;     //Ű�� �ޱ����� ����
    float vAxis;     //Ű�� �ޱ����� ����

    float SprintSpeed = 10.6f;                          // �⺻ �޸��� �ӵ�
    float MoveSpeed = 4.0f;                            // �⺻ �ȴ� �ӵ�
    float jumppower = 10;                              // ��������

    bool isBorder;    //������ ���� ����    

    Vector3 moveVec;        //���� ���������� ����
    Vector3 dodgeVec;

    Rigidbody rigid;       // ����ȿ�� ����
    Animator anim;     //�ִϸ��̼� �ֱ� ���� �Լ�

    public float mouseSensitivity = 100f; // ���콺 �ΰ���
    private float xRotation = 0f; // ī�޶� ���� ȸ�� ������ ������ ����
    private float yRotation = 0f; // ī�޶� ���� ȸ�� ������ ������ ����

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

        StartCoroutine(DisableInputForSeconds(19)); // 17�ʰ� �Է� ����

        
    }

    IEnumerator DisableInputForSeconds(float seconds)
    {
        isInputEnabled = false;
        yield return new WaitForSeconds(seconds);
        isInputEnabled = true; // 5�� �� �Է� Ȱ��ȭ
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
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���� ���� ����

        yRotation += mouseX;

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // ī�޶� ���� ȸ��
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f); // ĳ���� �¿� ȸ��
    }

    public void Move()       // ĳ���� ������ ����
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (Input.GetKey(KeyCode.LeftShift))  // ���� shift�� ������ ���� �޾ƿ��� �Լ��̿�
        {
            isRun = true;
            applySpeed = SprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) // ���� shift�� ���� ���� �޾ƿ��� �Լ��̿�
        {
            isRun = false;
            applySpeed = MoveSpeed;
        }

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        anim.SetBool("isRun", isRun);
        anim.SetBool("isWalk", isMove);

        // �� �浹 �˻� ����
        if (isMove)
        {
            Vector3 lookForward = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z).normalized;
            Vector3 lookRight = new Vector3(playerCamera.transform.right.x, 0, playerCamera.transform.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;  // ĳ������ ���� ����

            // ������ �̵��� �� ���� ������ �ӵ��� 0���� ����
            if (Physics.Raycast(transform.position, lookForward, 10, LayerMask.GetMask("Wall")))
            {
                applySpeed = 0;  // ���� ������ �ӵ��� 0����
            }

            // �̵� ����
            transform.position += moveDir * Time.deltaTime * applySpeed;
            Debug.DrawRay(transform.position, lookForward * 10, Color.green);  // ����� ���� ǥ��
        }

    }

    public void Jump()
    {
        jump = Input.GetButtonDown("Jump");
        if (jump && !isJump)
        {

            rigid.AddForce(Vector3.up * jumppower, ForceMode.Impulse);  //�������� ���� ���ϴ� �Լ� �̿�
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
