using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // �⺻ ����
    public float maxHealth = 100;                     // �⺻ �ִ� ü��
    public float HealthRegen = 1;                     // �⺻ ü�� ���
    public float skillPower = 1;                      // �⺻ ��ų ������

    // ���ݰ���
    public float attackPower = 10;                    // �⺻ ���ݷ�
    public float attackSpeed = 1;                     // �⺻ ���� �ӵ�
    public float attackrate = 1;                     // �⺻ ���� ����


    // �̵�����
    public float MoveSpeed = 10.0f;                      // �⺻ �̵� �ӵ�
    public float SprintSpeed = 25.335f;                  // �⺻ �޸��� �ӵ�
    public float JumpHeight = 1.2f;                     // ��������


    public float skillCooldown = 5;                     // �⺻ ��Ÿ��
    public float startingMoney = 0;                     // �ʱ� ��
    public float startingspirits = 0;                   // �ʱ� ��ȥ

    // �⺻ �Ӽ� ���ݷ�
    public float baseFireAttackPower = 0;             // �⺻ ȭ�� ���ݷ�
    public float basePoisonAttackPower = 0;           // �⺻ �ߵ� ���ݷ�
    public float baseShockAttackPower = 0;            // �⺻ ���� ���ݷ�
    public float baseBleedAttackPower = 0;            // �⺻ ���� ���ݷ�
    public float baseFreezeAttackPower = 0;           // �⺻ ���� ���ݷ�

    // ���� ���� ����
    public float CurrentHealth;                       // ���� ü��
    public float CurrentMoney;                        // ���� ��
    public float Currentspirits;                      // ���� ��ȥ


    // �߰� ĳ���� ����
    public float additionalAttackPower = 0;         // �߰� ���ݷ�
    public float additionalHealthRegen = 0;         // �߰� ü�� ���
    public float additionalMovementSpeed = 0;       // �߰� �̵� �ӵ�
    public float additionalAttackSpeed = 0;         // �߰� ���� �ӵ�
    public float additionalSkillCooldown = 0;       // �߰� ��Ÿ��

    public float additionalFireAttackPower = 0;       // �߰� ȭ�� ���ݷ�
    public float additionalPoisonAttackPower = 0;     // �߰� �ߵ� ���ݷ�
    public float additionalShockAttackPower = 0;      // �߰� ���� ���ݷ�
    public float additionalBleedAttackPower = 0;      // �߰� ���� ���ݷ�
    public float additionalFreezeAttackPower = 0;     // �߰� ���� ���ݷ�



    public float criticalChance = 0.05f;            // ġ��Ÿ Ȯ��
    public float criticalMultiplier = 2.0f;         // ġ��Ÿ ����



    //�߰� ��ȭ ȹ�� 
    public float additionalspiritGainPercentage = 0; // �߰� ��ȥ ���淮 
    public float additionalGoldGainPercentage = 0;   // �߰� ��� ���淮 

    //������ ����Ȯ��
    public float additionalspiritSpawnChance = 0;    // �߰� ��ȥ ������ 
    public float additionalGoldSpawnChance = 0;      // �߰� ��� ������ 
    public float jumppower = 10;                     // ��������


    private float lastRegenTime;                     // ������ ü�� ȸ�� �ð�
    private float regenInterval = 1.0f;              // ü�� ȸ�� ���� (��)
    private float regenAmount = 1.0f;                // ü�� ȸ����



    private void Start()
    {
        CurrentHealth = maxHealth;
        CurrentMoney = startingMoney;
        Currentspirits = startingspirits;

    }




    private void Update()
    {
        // ���� ���ݸ��� ü���� ȸ���մϴ�.
        if (Time.time - lastRegenTime >= regenInterval)
        {
            lastRegenTime = Time.time;
            RegenerateHealth();
        }
    }

    private void RegenerateHealth()
    {
        // ���� ü���� �ִ� ü�º��� ���� ���� ü���� ȸ���մϴ�.
        if (CurrentHealth < maxHealth)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + regenAmount, maxHealth); // �ִ� ü���� ���� �ʵ��� �մϴ�.
        }
    }

}