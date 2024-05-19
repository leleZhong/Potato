using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]       //����Ƽ ���ο��� Ȯ��
    public Transform characterBody; // �ش�Ǵ� ��ü�� �巡�� �� �����ϸ� �°� ���
    public Transform cameraArm;  // �ش�Ǵ� ��ü�� �巡�� �� �����ϸ� �°� ���
    public float applySpeed; // ����Ǵ� �ӵ��� ������ ����
    public CharacterStats characterStats; //ĳ���� ���ȿ��� �ʿ��� �� �޾ƿ��� ���� ����

    bool isRun;       //�޸��� ���� Ȯ�� ����

    bool jump;       // ���� ���� Ȯ�� ����
    bool isJump;     // �������� ���� �����ϰ� �ϴ� ����

    bool fDown;       // fŰ�� �������� ��ȣ�ۿ��ϴ� ����

    bool dodge;       // ȸ�� ���� Ȯ�� ����
    bool isDodge;     // ȸ�� �����ϰ� �ϴ� ����

    bool attack;       // ���� ���� Ȯ�� ����
    bool isattack;     // ���� �����ϰ� �ϴ� ����

    float hAxis;     //Ű�� �ޱ����� ����
    float vAxis;     //Ű�� �ޱ����� ����

    float comboCount;  //�޺������� ���� ���� 

    bool Click;  // Ŭ���� ������ ������ ���� ����
    float fireDelay; // ���ݵ����̸� ����� ���� ����
    bool isFireReady; // ���ݿ��θ� Ȯ���ϱ� ���� ����

    bool isBorder;    //������ ���� ����    

    float maxComboCount = 3;

    public GameObject[] Passive; // �нú� �������� �Ծ��� ���� ����Ǵ� �迭
    public bool[] HasPassive;  // �нú� �������� �Ծ��� ���� ���ΰ� ����Ǵ� �迭

    Vector3 moveVec;        //���� ���������� ����
    Vector3 dodgeVec;

    Rigidbody rigid;       // ����ȿ�� ����
    Animator anim;     //�ִϸ��̼� �ֱ� ���� �Լ�
    GameObject nearObject; //Ʈ���� �� ������ ��������

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = characterBody.GetComponent<Animator>();
        applySpeed = characterStats.MoveSpeed;
    }
    void Update()
    {
        Aim();
        Move();
        Jump();
        Dodge();
        MeleeAttack();


    }
    public void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical'");
        fDown = Input.GetButtonDown("Interaction");
        Click = Input.GetButtonDown("Fire1");
    }

    public void Aim()  // tps ȭ�� ������ ����
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); //Input.GetAxis("Mouse X");  ���콺�� ��ȭ���� �޾ƿ��� x�� �Լ�
                                                                                              //Input.GetAxis("Mouse Y");  ���콺�� ��ȭ���� �޾ƿ��� y�� �Լ�
        Vector3 camAngle = cameraArm.rotation.eulerAngles;    //���Ϸ� ���� �̿��ؼ� ���콺 �Է°� �ޱ�
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f) //ī�޶��� ������ �ٲ�� ������ �������� ���ѵδ� �Լ�   //ã�ƺ��� clamp mathf f�������? ������ ������
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z); // ���� �Է°����� ��� �� �����ϴ� �Լ�

    }

    public void Move()       // tps ĳ���� ������ ����
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;

        if (Input.GetKey(KeyCode.LeftShift))  // ���� shift�� ������ ���� �޾ƿ��� �Լ��̿�
        {
            isRun = true;
            applySpeed = characterStats.SprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) // ���� shift�� ���� ���� �޾ƿ��� �Լ��̿�
        {
            isRun = false;
            applySpeed = characterStats.MoveSpeed;
        }

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // Ű���޾ƿ���
        bool isMove = moveInput.magnitude != 0;
        anim.SetBool("isRun", isRun);
        anim.SetBool("isWalk", isMove);

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z).normalized; // ȭ�� ������ ���� ������ �޾ƿ��� ���Ʒ��� ����
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;


            characterBody.forward = lookForward;
            if (!isBorder)
                transform.position += moveDir * Time.deltaTime * applySpeed;
            Debug.DrawRay(transform.position, lookForward, Color.green);
            isBorder = Physics.Raycast(transform.position, lookForward, 1, LayerMask.GetMask("Wall"));

        }
    }

    public void Jump()
    {
        jump = Input.GetButtonDown("Jump");
        if (jump && !isJump && !isDodge)
        {

            rigid.AddForce(Vector3.up * characterStats.jumppower, ForceMode.Impulse);  //�������� ���� ���ϴ� �Լ� �̿�
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
    public void Dodge()
    {
        dodge = Input.GetKey(KeyCode.C);
        if (dodge && !isJump && !isDodge)
        {
            StartCoroutine(PerformDodge());
        }
    }

    IEnumerator PerformDodge()
    {
        characterStats.MoveSpeed *= 2f;
        characterStats.SprintSpeed *= 2f;
        anim.SetTrigger("doDodge");
        isDodge = true;

        yield return new WaitForSeconds(0.5f);

        characterStats.MoveSpeed *= 0.5f;
        characterStats.SprintSpeed *= 0.5f;
        isDodge = false;
    }


    void MeleeAttack() // ���� ���� ����� �����ϱ� ���� �Լ�
    {
        // ���콺 ���� ��ư�� Ŭ���ߴ��� �˻�
        attack = Input.GetMouseButtonDown(0);
        fireDelay += Time.deltaTime;
        isFireReady = characterStats.attackrate < fireDelay;

        // ���콺 Ŭ���ϰ�, ���� ��� �����̸�, ȸ�� ���� �ƴ� ��
        if (attack && isFireReady && !isDodge)
        {
            // �޺� ī��Ʈ�� �ִ� �޺� Ƚ������ ���� ���� ����
            if (comboCount < maxComboCount)
            {
                StartCoroutine(MeleeCombo());
                anim.SetTrigger("doSwing"); // �⺻���� ���� ���� �ִϸ��̼� ����
                fireDelay = 0;
            }
        }
    }

    IEnumerator MeleeCombo()
    {
        comboCount++;

        // �޺� ī��Ʈ�� ���� �ش��ϴ� ���� ���� �ڷ�ƾ ����
        if (comboCount == 1)
        {
            yield return StartCoroutine(MeleeAttack("doDodge")); // �� ��° ���� �ִϸ��̼� ����
        }
        else if (comboCount == 2)
        {
            yield return StartCoroutine(MeleeAttack("doSwing3")); // �� ��° ���� �ִϸ��̼� ����
            comboCount = 0; // �� ��° ���� ���� �޺� ī��Ʈ �ʱ�ȭ
        }
    }

    IEnumerator MeleeAttack(string animationTrigger)
    {
        // �� ���� ���ݿ� ���� �ڵ�
        // �ʿ信 ���� Ư�� �ִϸ��̼� �� ������ �����ϼ���

        anim.SetTrigger(animationTrigger);

        yield return new WaitForSeconds(0.5f);

        // �� ���� ���ݿ� ���� �߰� ����
    }


    void BulletAttack()
    {
        // ���콺 ���� ��ư�� Ŭ���ߴ��� �˻�
        attack = Input.GetMouseButtonDown(0);
        fireDelay += Time.deltaTime;
        isFireReady = characterStats.attackrate < fireDelay;

        // ���콺 Ŭ���ϰ�, ���� ��� �����̸�, ȸ�� ���� �ƴ� ��
        if (attack && isFireReady && !isDodge)
        {
            StartCoroutine(Bullet());
            anim.SetTrigger("doShoot");
            fireDelay = 0;

        }
    }

    IEnumerator Bullet()
    {
        // ���Ÿ� ������ �⺻ ����
        // ����ü �߻� ���� �ڵ带 �߰��ϼ���

        yield return new WaitForSeconds(0.5f);

        // �߰� ���� (�ʿ信 ���� ����)
    }
}
