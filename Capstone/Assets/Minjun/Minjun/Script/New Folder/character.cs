using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    public Transform characterBody;  // ĳ������ ��ü Transform
    public Transform cameraArm;  // ī�޶� �� Transform
    public float moveSpeed = 5f;  // �Ϲ� �̵� �ӵ�
    public float sprintSpeed = 10f;  // �޸��� �ӵ�
    public float jumpPower = 7f;  // ���� ��
    public float applySpeed;  // ����� �̵� �ӵ�

    bool isRun;  // �޸��� ����
    bool jump;  // ���� �Է� ����
    bool isJump;  // ���� �� ����
    bool fDown;  // ��ȣ�ۿ� �Է� ����

    float hAxis;  // ���� �Է� ��
    float vAxis;  // ���� �Է� ��

    bool isBorder;  // ��� �浹 ����

    Vector3 moveVec;  // �̵� ����
    Rigidbody rigid;  // ������ٵ� ������Ʈ
    Animator anim;  // �ִϸ����� ������Ʈ

    void Start()
    {
        // ������ٵ� �� �ִϸ����� ������Ʈ �ʱ�ȭ
        rigid = GetComponent<Rigidbody>();
        anim = characterBody.GetComponent<Animator>();
        applySpeed = moveSpeed;
    }

    void Update()
    {
        // �� �����Ӹ��� �Է�, ����, �̵�, ���� ��� ȣ��
        GetInput();
        Aim();
        Move();
        Jump();
    }

    void GetInput()
    {
        // �÷��̾� �Է� ó��
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        // fDown = Input.GetButtonDown("Interaction");  // ��ȣ�ۿ� �Է� ���� �Ǵ� �ٸ� ��ư���� ��ü
    }

    void Aim()
    {
        // ���콺 �����ӿ� ���� ī�޶� ����
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
        // �̵� ���� ��� �� �ִϸ��̼� ����
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
            applySpeed = sprintSpeed;
            anim.SetBool("isRunning", true);
        }
        else
        {
            isRun = false;
            applySpeed = moveSpeed;
            anim.SetBool("isRunning", false);
        }

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        anim.SetBool("isWalking", isMove);

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = lookForward;
            if (!isBorder)
                transform.position += moveDir * Time.deltaTime * applySpeed;
            UnityEngine.Debug.DrawRay(transform.position, lookForward, Color.green);
            isBorder = Physics.Raycast(transform.position, lookForward, 1, LayerMask.GetMask("Wall"));
        }
    }

    void Jump()
    {
        // ���� �Է� ó�� �� �ִϸ��̼� ����
        jump = Input.GetButtonDown("Jump");
        if (jump && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("isJumping", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ���� �� ���� ���� �ʱ�ȭ
        if (collision.gameObject.tag == "ground")
        {
            isJump = false;
            anim.SetBool("isJumping", false);
        }
    }
}
