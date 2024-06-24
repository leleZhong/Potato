using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_new : MonoBehaviour
{
    [SerializeField]       //����Ƽ ���ο��� Ȯ��
    public Transform characterBody; // �ش�Ǵ� ��ü�� �巡�� �� �����ϸ� �°� ���
    public Camera playerCamera; // ī�޶� ������Ʈ�� �ν����Ϳ��� ����
    public float applySpeed; // ����Ǵ� �ӵ��� ������ ����


    bool isRun;       //�޸��� ���� Ȯ�� ����
    bool jump;       // ���� ���� Ȯ�� ����
    bool isJump;     // �������� ���� �����ϰ� �ϴ� ����


    bool fDown;       // fŰ�� �������� ��ȣ�ۿ��ϴ� ����

    bool attack;       // ���� ���� Ȯ�� ����
    bool isattack;     // ���� �����ϰ� �ϴ� ����

    float hAxis;     //Ű�� �ޱ����� ����
    float vAxis;     //Ű�� �ޱ����� ����

    float SprintSpeed = 53f;                          // �⺻ �޸��� �ӵ�
    float MoveSpeed = 20f;                            // �⺻ �ȴ� �ӵ�
    float jumppower = 10;                              // ��������

    bool isBorder;    //������ ���� ����    

    Vector3 moveVec;        //���� ���������� ����
    Vector3 dodgeVec;

    Rigidbody rigid;       // ����ȿ�� ����
    Animator anim;     //�ִϸ��̼� �ֱ� ���� �Լ�

    public float mouseSensitivity = 100f; // ���콺 �ΰ���
    private float xRotation = 0f; // ī�޶� ���� ȸ�� ������ ������ ����
    private float yRotation = 0f; // ī�޶� ���� ȸ�� ������ ������ ����

    void Awake()
    {
        playerCamera = Camera.main;
        rigid = GetComponent<Rigidbody>();
        anim = characterBody.GetComponent<Animator>();
    }

    void Start()
    {
        applySpeed = 20f;
    }
    void Update()
    {
        Aim();
        Move();
        Jump();


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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isJump = false;
            anim.SetBool("isJump", false);



        }
    }


}