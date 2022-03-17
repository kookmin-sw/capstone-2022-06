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

    // Skill_E 인풋 변수
    Vector3 position;
    public Canvas skill_ECanvas;
    public Image skillshot;
    public Transform player;

    [Header("Skill_R")]
    public Image skillImage_R;
    public float cooldown_R = 80;
    bool isCooldown_R = false;
    public KeyCode skill_R;

    // Skill_R 인풋 변수
    public Image targetCircle;
    public Image indicatorRangeCircle;
    public Canvas skill_RCanvas;
    private Vector3 posUp;
    public float maxSkill_RDistance;

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

        // 초기 스킬샷 UI enable
        skillshot.GetComponent<Image>().enabled = false;
        targetCircle.GetComponent<Image>().enabled = false;
        indicatorRangeCircle.GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        Skill_Q();
        Skill_W();
        Skill_E();
        Skill_R();
        Ability_D();
        Ability_F();

        // 마우스 스킬 입력
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Skill_E 입력
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        // Skill_E 캔버스 입력
        Quaternion transRot = Quaternion.LookRotation(position - player.transform.position);
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, 0);
        skill_ECanvas.transform.rotation = Quaternion.Lerp(transRot, skill_ECanvas.transform.rotation, 0f);

        // Skill_R 입력
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.gameObject != this.gameObject)
            {
                posUp = new Vector3(hit.point.x, 10f, hit.point.z);
                position = hit.point;
            }
        }

        // Skill_R 캔버스 입력
        var hitPosDir = (hit.point - transform.position).normalized;
        float distance = Vector3.Distance(hit.point, transform.position);
        distance = Mathf.Min(distance, maxSkill_RDistance);

        var newHitPos = transform.position + (hitPosDir * distance);
        skill_RCanvas.transform.position = (newHitPos);
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
            skillshot.GetComponent<Image>().enabled = true;

            // 다른 스킬샷 UI disable
            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;
        }

        if (skillshot.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            isCooldown_E = true;
            skillImage_E.fillAmount = 1;
        }

        if (isCooldown_E)
        {
            skillImage_E.fillAmount -= 1 / cooldown_E * Time.deltaTime;
            skillshot.GetComponent<Image>().enabled = false;

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
            indicatorRangeCircle.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;

            // 다른 스킬샷 UI disable
            skillshot.GetComponent<Image>().enabled = false;
        }

        if(targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            isCooldown_R = true;
            skillImage_R.fillAmount = 1;
        }

        if (isCooldown_R)
        {
            skillImage_R.fillAmount -= 1 / cooldown_R * Time.deltaTime;

            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;

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
