using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]       //유니티 내부에서 확인
    public Transform characterBody; // 해당되는 개체를 드래그 후 지정하면 맞게 사용
    public Transform cameraArm;  // 해당되는 개체를 드래그 후 지정하면 맞게 사용
    public float applySpeed; // 적용되는 속도를 변수로 만듦
    public CharacterStats characterStats; //캐릭터 스탯에서 필요한 값 받아오기 위한 변수

    bool isRun;       //달리기 여부 확인 변수

    bool jump;       // 점프 여부 확인 변수
    bool isJump;     // 땅에서만 점프 가능하게 하는 변수

    bool fDown;       // f키를 눌렀을때 상호작용하는 변수

    bool dodge;       // 회피 여부 확인 변수
    bool isDodge;     // 회피 가능하게 하는 변수

    bool attack;       // 공격 여부 확인 변수
    bool isattack;     // 공격 가능하게 하는 변수

    float hAxis;     //키값 받기위한 변수
    float vAxis;     //키값 받기위한 변수

    float comboCount;  //콤보구현을 위한 변수 

    bool Click;  // 클릭을 했을떄 공격을 위한 변수
    float fireDelay; // 공격딜레이를 만들기 위한 변수
    bool isFireReady; // 공격여부를 확인하기 위한 변수

    bool isBorder;    //벽관통 막는 변수    

    float maxComboCount = 3;

    public GameObject[] Passive; // 패시브 아이템을 먹었을 때의 저장되는 배열
    public bool[] HasPassive;  // 패시브 아이템을 먹었을 때의 여부가 저장되는 배열

    Vector3 moveVec;        //조건 설정을위한 백터
    Vector3 dodgeVec;

    Rigidbody rigid;       // 물리효과 구현
    Animator anim;     //애니메이션 넣기 위한 함수
    GameObject nearObject; //트리거 될 아이템 변수선언

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

    public void Aim()  // tps 화면 움직임 구현
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); //Input.GetAxis("Mouse X");  마우스의 변화값을 받아오는 x축 함수
                                                                                              //Input.GetAxis("Mouse Y");  마우스의 변화값을 받아오는 y축 함수
        Vector3 camAngle = cameraArm.rotation.eulerAngles;    //오일러 각을 이용해서 마우스 입력값 받기
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f) //카메라의 시점이 바뀌는 현상을 막기위해 제한두는 함수   //찾아볼꺼 clamp mathf f사용이유? 각도로 추측중
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z); // 실제 입력값으로 계산 후 적용하는 함수

    }

    public void Move()       // tps 캐릭터 움직임 구현
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = dodgeVec;

        if (Input.GetKey(KeyCode.LeftShift))  // 좌측 shift를 누르면 값을 받아오는 함수이용
        {
            isRun = true;
            applySpeed = characterStats.SprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) // 좌측 shift를 떼면 값을 받아오는 함수이용
        {
            isRun = false;
            applySpeed = characterStats.MoveSpeed;
        }

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // 키값받아오기
        bool isMove = moveInput.magnitude != 0;
        anim.SetBool("isRun", isRun);
        anim.SetBool("isWalk", isMove);

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z).normalized; // 화면 돌리는 것의 방향을 받아오되 위아래는 제외
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

            rigid.AddForce(Vector3.up * characterStats.jumppower, ForceMode.Impulse);  //물리적인 힘을 가하는 함수 이용
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


    void MeleeAttack() // 근접 공격 기능을 구현하기 위한 함수
    {
        // 마우스 왼쪽 버튼을 클릭했는지 검사
        attack = Input.GetMouseButtonDown(0);
        fireDelay += Time.deltaTime;
        isFireReady = characterStats.attackrate < fireDelay;

        // 마우스 클릭하고, 공격 대기 상태이며, 회피 중이 아닐 때
        if (attack && isFireReady && !isDodge)
        {
            // 콤보 카운트가 최대 콤보 횟수보다 작을 때만 실행
            if (comboCount < maxComboCount)
            {
                StartCoroutine(MeleeCombo());
                anim.SetTrigger("doSwing"); // 기본적인 근접 공격 애니메이션 실행
                fireDelay = 0;
            }
        }
    }

    IEnumerator MeleeCombo()
    {
        comboCount++;

        // 콤보 카운트에 따라 해당하는 근접 공격 코루틴 실행
        if (comboCount == 1)
        {
            yield return StartCoroutine(MeleeAttack("doDodge")); // 두 번째 공격 애니메이션 실행
        }
        else if (comboCount == 2)
        {
            yield return StartCoroutine(MeleeAttack("doSwing3")); // 세 번째 공격 애니메이션 실행
            comboCount = 0; // 세 번째 공격 이후 콤보 카운트 초기화
        }
    }

    IEnumerator MeleeAttack(string animationTrigger)
    {
        // 각 근접 공격에 대한 코드
        // 필요에 따라 특정 애니메이션 및 동작을 정의하세요

        anim.SetTrigger(animationTrigger);

        yield return new WaitForSeconds(0.5f);

        // 각 근접 공격에 대한 추가 동작
    }


    void BulletAttack()
    {
        // 마우스 왼쪽 버튼을 클릭했는지 검사
        attack = Input.GetMouseButtonDown(0);
        fireDelay += Time.deltaTime;
        isFireReady = characterStats.attackrate < fireDelay;

        // 마우스 클릭하고, 공격 대기 상태이며, 회피 중이 아닐 때
        if (attack && isFireReady && !isDodge)
        {
            StartCoroutine(Bullet());
            anim.SetTrigger("doShoot");
            fireDelay = 0;

        }
    }

    IEnumerator Bullet()
    {
        // 원거리 공격의 기본 동작
        // 투사체 발사 등의 코드를 추가하세요

        yield return new WaitForSeconds(0.5f);

        // 추가 동작 (필요에 따라 수정)
    }
}
