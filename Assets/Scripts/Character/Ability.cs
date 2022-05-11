using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    /*
        패시브 : 레벨에 비례한 추가 체력재생이 적용된다.
        스킬 Q : 사용 시 3초간 공격력 상승
        스킬 W : 사용 시 자신의 생명력 회복
        스킬 E : 사용 시 지정된 방향으로 검기 발사
        스킬 R : 사용 시 지정된 위치에 거대한 검을 떨어뜨림
     */

    Stats stats;
    PlayerAnimation playerAnim;
    ClickMovement moveScript;
    HeroCombat heroCombat;

    public bool isPassive = false;

    public GameObject skillUpButton;
    public Image Q_SkillPoint1;
    public Image Q_SkillPoint2;
    public Image Q_SkillPoint3;
    public Image W_SkillPoint1;
    public Image W_SkillPoint2;
    public Image W_SkillPoint3;
    public Image E_SkillPoint1;
    public Image E_SkillPoint2;
    public Image E_SkillPoint3;
    public Image R_SKillPoint1;

    // Component에서 스킬의 쿨타임, 인풋 키 설정

    [Header("Skill_Q")]
    public Image skillImage_Q;
    public float cooldown_Q = 5;
    bool isCooldown_Q = false;
    public KeyCode skill_Q;
    public int skillPoint_Q;

    // Skill_Q 인풋 변수
    public ParticleSystem fire;
    public bool isActive = false;

    [Header("Skill_W")]
    public Image skillImage_W;
    public float cooldown_W = 7;
    bool isCooldown_W = false;
    public KeyCode skill_W;
    public int skillPoint_W;

    [Header("Skill_E")]
    public Image skillImage_E;
    public float cooldown_E = 5;
    bool isCooldown_E = false;
    public KeyCode skill_E;
    bool canSkillshot_E = true;
    public GameObject projPrefab_E;
    public Transform projSpawnPoint_E;
    public bool isSkill_E = false;
    public int skillPoint_E;

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
    bool canSkillshot_R = true;
    public GameObject projPrefab_R;
    public Transform projSpawnPoint_R;
    public bool isSkill_R = false;
    public int skillPoint_R;

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

        stats = GetComponent<Stats>();
        playerAnim = GetComponent<PlayerAnimation>();
        moveScript = GetComponent<ClickMovement>();
        heroCombat = GetComponent<HeroCombat>();
    }

    void Update()
    {
        SkillUP();
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
        transRot.eulerAngles = new Vector3(0, transRot.eulerAngles.y, transRot.eulerAngles.z);
        skill_ECanvas.transform.rotation = Quaternion.Lerp(transRot, skill_ECanvas.transform.rotation, 0f);

        // Skill_R 입력
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject != this.gameObject)
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
        newHitPos.y = 0;
        skill_RCanvas.transform.position = (newHitPos);
    }

    void Skill_Q()
    {
        if(skillPoint_Q < 1)
        {
            return;
        }

        // 스킬Q의 쿨타임 UI 이미지
        if (Input.GetKey(skill_Q) && isCooldown_Q == false)
        {
            StartCoroutine("FireAttack");

            isCooldown_Q = true;
            skillImage_Q.fillAmount = 1;
        }

        if (isCooldown_Q)
        {
            skillImage_Q.fillAmount -= 1 / cooldown_Q * Time.deltaTime;

            if (skillImage_Q.fillAmount <= 0)
            {
                skillImage_Q.fillAmount = 0;
                isCooldown_Q = false;
            }
        }
    }

    IEnumerator FireAttack()
    {
        // 스킬Q사용 시 3초간 파티클과 공격력 상승
        isActive = true;
        float originAttackDmg = stats.attackDmg;
        stats.attackDmg += stats.attackDmg * 0.5f;
        fire.Play();
        
        yield return new WaitForSeconds(3.0f);
        isActive = false;
        stats.attackDmg = originAttackDmg;
        fire.Stop();
    }

    void Skill_W()
    {
        if (skillPoint_W < 1)
        {
            return;
        }

        // 스킬W의 쿨타임 UI 이미지
        if (Input.GetKey(skill_W) && isCooldown_W == false)
        {
            // 생명력++
            stats.health += 50 + (stats.abilityPower * 0.5f);

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
        if (skillPoint_E < 1)
        {
            return;
        }

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
            isSkill_E = true;
            heroCombat.targetedEnemy = null;

            // Rotate
            Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            moveScript.agent.SetDestination(transform.position);
            moveScript.agent.stoppingDistance = 0;

            if(canSkillshot_E)
            {
                isCooldown_E = true;
                skillImage_E.fillAmount = 1;

                // 애니메이션
                StartCoroutine(corSkill_E());
            }
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

    IEnumerator corSkill_E()
    {
        canSkillshot_E = false;
        playerAnim.anim.SetBool("skill_E", true);

        yield return new WaitForSeconds(0.55f);

        playerAnim.anim.SetBool("skill_E", false);
        isSkill_E = false;
        canSkillshot_E = true;
    }

    // 스킬 E 애니메이션 이벤트
    public void SpawnSkill_E()
    {
        Instantiate(projPrefab_E, projSpawnPoint_E.transform.position, projSpawnPoint_E.transform.rotation);
    }

    void Skill_R()
    {
        if (skillPoint_R < 1)
        {
            return;
        }

        // 스킬R의 쿨타임 UI 이미지
        if (Input.GetKey(skill_R) && isCooldown_R == false)
        {
            indicatorRangeCircle.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;

            // 다른 스킬샷 UI disable
            skillshot.GetComponent<Image>().enabled = false;
        }

        if (targetCircle.GetComponent<Image>().enabled == true && Input.GetMouseButtonDown(0))
        {
            isSkill_R = true;
            heroCombat.targetedEnemy = null;

            isCooldown_R = true;
            skillImage_R.fillAmount = 1;

            // Rotate
            Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            moveScript.agent.SetDestination(transform.position);
            moveScript.agent.stoppingDistance = 0;

            if (canSkillshot_R)
            {
                isCooldown_R = true;
                skillImage_R.fillAmount = 1;

                // 애니메이션
                StartCoroutine(corSkill_R());
            }
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

    IEnumerator corSkill_R()
    {
        canSkillshot_R = false;
        playerAnim.anim.SetBool("skill_R", true);

        yield return new WaitForSeconds(1.0f);

        playerAnim.anim.SetBool("skill_R", false);
        isSkill_R = false;
        canSkillshot_R = true;
    }

    // 스킬 E 애니메이션 이벤트
    public void SpawnSkill_R()
    {
        Instantiate(projPrefab_R, projSpawnPoint_R.transform.position, projSpawnPoint_R.transform.rotation);
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

    void SkillUP()
    {
        if(stats.skillPoint >= 1)
        {
            Debug.Log("skillup");
            skillUpButton.SetActive(true);
        }
        else
        {
            skillUpButton.SetActive(false);
        }
    }

    public void SkillPointUp_Q()
    {
        if(skillPoint_Q == 3)
        {
            return;
        }

        skillPoint_Q++;
        stats.skillPoint--;
        if (skillPoint_Q == 1)
        {
            Q_SkillPoint1.color = Color.yellow;
        }
        else if (skillPoint_Q == 2)
        {
            Q_SkillPoint2.color = Color.yellow;
        }
        else if (skillPoint_Q == 3)
        {
            Q_SkillPoint3.color = Color.yellow;
        }
    }

    public void SkillPointUp_W()
    {
        if (skillPoint_W == 3)
        {
            return;
        }

        skillPoint_W++;
        stats.skillPoint--;
        if (skillPoint_W == 1)
        {
            W_SkillPoint1.color = Color.yellow;
        }
        else if (skillPoint_W == 2)
        {
            W_SkillPoint2.color = Color.yellow;
        }
        else if (skillPoint_W == 3)
        {
            W_SkillPoint3.color = Color.yellow;
        }
    }

    public void SkillPointUp_E()
    {
        if (skillPoint_E == 3)
        {
            return;
        }

        skillPoint_E++;
        stats.skillPoint--;
        if (skillPoint_E == 1)
        {
            E_SkillPoint1.color = Color.yellow;
        }
        else if (skillPoint_E == 2)
        {
            E_SkillPoint2.color = Color.yellow;
        }
        else if (skillPoint_E == 3)
        {
            E_SkillPoint3.color = Color.yellow;
        }
    }

    public void SkillPointUp_R()
    {
        if (skillPoint_R == 1 || stats.level < 6)
        {
            return;
        }

        skillPoint_R++;
        stats.skillPoint--;
        R_SKillPoint1.color = Color.yellow;
    }
}
