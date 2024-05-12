using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // 기본 스탯
    public float maxHealth = 100;                     // 기본 최대 체력
    public float HealthRegen = 1;                     // 기본 체력 재생
    public float skillPower = 1;                      // 기본 스킬 데미지

    // 공격관련
    public float attackPower = 10;                    // 기본 공격력
    public float attackSpeed = 1;                     // 기본 공격 속도
    public float attackrate = 1;                     // 기본 공격 범위


    // 이동관련
    public float MoveSpeed = 10.0f;                      // 기본 이동 속도
    public float SprintSpeed = 25.335f;                  // 기본 달리기 속도
    public float JumpHeight = 1.2f;                     // 점프높이


    public float skillCooldown = 5;                     // 기본 쿨타임
    public float startingMoney = 0;                     // 초기 돈
    public float startingspirits = 0;                   // 초기 영혼

    // 기본 속성 공격력
    public float baseFireAttackPower = 0;             // 기본 화상 공격력
    public float basePoisonAttackPower = 0;           // 기본 중독 공격력
    public float baseShockAttackPower = 0;            // 기본 감전 공격력
    public float baseBleedAttackPower = 0;            // 기본 출혈 공격력
    public float baseFreezeAttackPower = 0;           // 기본 빙결 공격력

    // 현재 보유 스탯
    public float CurrentHealth;                       // 현재 체력
    public float CurrentMoney;                        // 현재 돈
    public float Currentspirits;                      // 현재 영혼


    // 추가 캐릭터 스탯
    public float additionalAttackPower = 0;         // 추가 공격력
    public float additionalHealthRegen = 0;         // 추가 체력 재생
    public float additionalMovementSpeed = 0;       // 추가 이동 속도
    public float additionalAttackSpeed = 0;         // 추가 공격 속도
    public float additionalSkillCooldown = 0;       // 추가 쿨타임

    public float additionalFireAttackPower = 0;       // 추가 화상 공격력
    public float additionalPoisonAttackPower = 0;     // 추가 중독 공격력
    public float additionalShockAttackPower = 0;      // 추가 감전 공격력
    public float additionalBleedAttackPower = 0;      // 추가 출혈 공격력
    public float additionalFreezeAttackPower = 0;     // 추가 빙결 공격력



    public float criticalChance = 0.05f;            // 치명타 확률
    public float criticalMultiplier = 2.0f;         // 치명타 배율



    //추가 재화 획득 
    public float additionalspiritGainPercentage = 0; // 추가 영혼 습득량 
    public float additionalGoldGainPercentage = 0;   // 추가 골드 습득량 

    //아이템 생성확률
    public float additionalspiritSpawnChance = 0;    // 추가 영혼 생성량 
    public float additionalGoldSpawnChance = 0;      // 추가 골드 생성량 
    public float jumppower = 10;                     // 점프세기


    private float lastRegenTime;                     // 마지막 체력 회복 시간
    private float regenInterval = 1.0f;              // 체력 회복 간격 (초)
    private float regenAmount = 1.0f;                // 체력 회복량



    private void Start()
    {
        CurrentHealth = maxHealth;
        CurrentMoney = startingMoney;
        Currentspirits = startingspirits;

    }




    private void Update()
    {
        // 일정 간격마다 체력을 회복합니다.
        if (Time.time - lastRegenTime >= regenInterval)
        {
            lastRegenTime = Time.time;
            RegenerateHealth();
        }
    }

    private void RegenerateHealth()
    {
        // 현재 체력이 최대 체력보다 작을 때만 체력을 회복합니다.
        if (CurrentHealth < maxHealth)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + regenAmount, maxHealth); // 최대 체력을 넘지 않도록 합니다.
        }
    }

}