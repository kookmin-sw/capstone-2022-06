using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    // Component에서 스킬의 쿨타임, 인풋 키 설정
    
    [Header("Skill_Q")]
    public Image skillImage_Q;
    public float cooldown_Q = 3;
    bool isCooldown_Q = false;
    public KeyCode skill_Q;

    [Header("Skill_W")]
    public Image skillImage_W;
    public float cooldown_W = 7;
    bool isCooldown_W = false;
    public KeyCode skill_W;

    [Header("Skill_E")]
    public Image skillImage_E;
    public float cooldown_E = 5;
    bool isCooldown_E = false;
    public KeyCode skill_E;

    [Header("Skill_R")]
    public Image skillImage_R;
    public float cooldown_R = 80;
    bool isCooldown_R = false;
    public KeyCode skill_R;

    [Header("Ability_D")]
    public Image abilityImage_D;
    public float cooldown_D = 120;
    bool isCooldown_D = false;
    public KeyCode ability_D;

    [Header("Ability_F")]
    public Image abilityImage_F;
    public float cooldown_F = 120;
    bool isCooldown_F = false;
    public KeyCode ability_F;

    void Start()
    {
        // 쿨타임 표시를 위한 초기 설정
        skillImage_Q.fillAmount = 0;
        skillImage_W.fillAmount = 0;
        skillImage_E.fillAmount = 0;
        skillImage_R.fillAmount = 0;
        abilityImage_F.fillAmount = 0;
        abilityImage_D.fillAmount = 0;
    }

    void Update()
    {
        Skill_Q();
        Skill_W();
        Skill_E();
        Skill_R();
        Ability_D();
        Ability_F();
    }

    void Skill_Q()
    {
        // 스킬Q의 쿨타임 UI 이미지
        if(Input.GetKey(skill_Q) && isCooldown_Q == false)
        {
            isCooldown_Q = true;
            skillImage_Q.fillAmount = 1;
        }

        if(isCooldown_Q)
        {
            skillImage_Q.fillAmount -= 1 / cooldown_Q * Time.deltaTime;

            if(skillImage_Q.fillAmount <= 0)
            {
                skillImage_Q.fillAmount = 0;
                isCooldown_Q = false;
            }
        }
    }

    void Skill_W()
    {
        // 스킬W의 쿨타임 UI 이미지
        if (Input.GetKey(skill_W) && isCooldown_W == false)
        {
            isCooldown_W = true;
            skillImage_W.fillAmount = 1;
        }

        if (isCooldown_W)
        {
            skillImage_W.fillAmount -= 1 / cooldown_W * Time.deltaTime;

            if (skillImage_W.fillAmount <= 0)
            {
                skillImage_W.fillAmount = 0;
                isCooldown_W = false;
            }
        }
    }

    void Skill_E()
    {
        // 스킬E의 쿨타임 UI 이미지
        if (Input.GetKey(skill_E) && isCooldown_E == false)
        {
            isCooldown_E = true;
            skillImage_E.fillAmount = 1;
        }

        if (isCooldown_E)
        {
            skillImage_E.fillAmount -= 1 / cooldown_E * Time.deltaTime;

            if (skillImage_E.fillAmount <= 0)
            {
                skillImage_E.fillAmount = 0;
                isCooldown_E = false;
            }
        }
    }

    void Skill_R()
    {
        // 스킬R의 쿨타임 UI 이미지
        if (Input.GetKey(skill_R) && isCooldown_R == false)
        {
            isCooldown_R = true;
            skillImage_R.fillAmount = 1;
        }

        if (isCooldown_R)
        {
            skillImage_R.fillAmount -= 1 / cooldown_R * Time.deltaTime;

            if (skillImage_R.fillAmount <= 0)
            {
                skillImage_R.fillAmount = 0;
                isCooldown_R = false;
            }
        }
    }

    void Ability_D()
    {
        // 어빌리티D의 쿨타임 UI 이미지
        if (Input.GetKey(ability_D) && isCooldown_D == false)
        {
            isCooldown_D = true;
            abilityImage_D.fillAmount = 1;
        }

        if (isCooldown_D)
        {
            abilityImage_D.fillAmount -= 1 / cooldown_D * Time.deltaTime;

            if (abilityImage_D.fillAmount <= 0)
            {
                abilityImage_D.fillAmount = 0;
                isCooldown_D = false;
            }
        }
    }

    void Ability_F()
    {
        // 어빌리티F의 쿨타임 UI 이미지
        if (Input.GetKey(ability_F) && isCooldown_F == false)
        {
            isCooldown_F = true;
            abilityImage_F.fillAmount = 1;
        }

        if (isCooldown_F)
        {
            abilityImage_F.fillAmount -= 1 / cooldown_D * Time.deltaTime;

            if (abilityImage_F.fillAmount <= 0)
            {
                abilityImage_F.fillAmount = 0;
                isCooldown_F = false;
            }
        }
    }
}
