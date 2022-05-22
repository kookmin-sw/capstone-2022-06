using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Ability : MonoBehaviour
{
    /*
        패시브 : 레벨에 비례한 추가 체력재생이 적용된다.
        스킬 Q : 사용 시 3초간 공격력 상승
        스킬 W : 사용 시 자신의 생명력 회복
        스킬 E : 사용 시 지정된 방향으로 검기 발사
        스킬 R : 사용 시 지정된 위치에 거대한 검을 떨어뜨림
     */

    //Stats stats;
    ChampionStat stat;
    PlayerAnimation playerAnim;
    ClickMovement moveScript;
    HeroCombat heroCombat;
    ChampionManager championManager;
    PhotonView PV;

    public bool isPassive = false;
    private bool isSkillReady = false;
    private Action registeredSkill;

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
    public Image R_SkillPoint1;

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

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        // 초기 스킬샷 UI enable
        skillshot.GetComponent<Image>().enabled = false;
        targetCircle.GetComponent<Image>().enabled = false;
        indicatorRangeCircle.GetComponent<Image>().enabled = false;

        stat = GetComponent<ChampionStat>();
        stat.Initialize("Mangoawl");
        playerAnim = GetComponent<PlayerAnimation>();
        moveScript = GetComponent<ClickMovement>();
        heroCombat = GetComponent<HeroCombat>();
        championManager = GetComponent<ChampionManager>();

        if (!PV.IsMine)
        {
            return;
        }

        ConnectToUI();

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
        if (!PV.IsMine)
        {
            return;
        }

        OnMouseClicked();
        SkillUP();
        OnButtonPressed();
        DecreaseCoolDown();

        // 마우스 스킬 입력
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Skill_E 입력
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            position = new Vector3(hit.point.x, 1f, hit.point.z);
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

    /// <summary>
    /// 마우스 클릭을 확인하는 메서드
    /// regiseteredSkill이 빈 상태가 아니면 콜백을 호출하고 액션을 비웁니다.
    /// </summary>
    void OnMouseClicked()
    {
        if (Input.GetMouseButton(0) && isSkillReady && registeredSkill != null)
        {
            registeredSkill();
            registeredSkill = null;
        }
    }

    /// <summary>
    /// 스킬과 관련된 키가 입력되었는지 확인하는 메서드.
    /// 매칭되는 스킬을 찾으면 해당 스킬을 호출합니다.
    /// </summary>
    void OnButtonPressed()
    {
        // 스킬 취소
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OffSkill();
            registeredSkill = null;
            return;
        }

        if (Input.GetKey(skill_Q))
        {
            Skill_Q();
        }
        else if (Input.GetKey(skill_W))
        {
            Skill_W();
        }
        else if (Input.GetKey(skill_E))
        {
            Skill_E();
        }
        else if (Input.GetKey(skill_R))
        {
            Skill_R();
        }
        else if (Input.GetKey(ability_D))
        {
            Ability_D();
        }
        else if (Input.GetKey(ability_F))
        {
            Ability_F();
        }
    }

    void DecreaseCoolDown()
    {
        if (isCooldown_Q)
        {
            skillImage_Q.fillAmount -= 1 / cooldown_Q * Time.deltaTime;

            if (skillImage_Q.fillAmount <= 0)
            {
                skillImage_Q.fillAmount = 0;
                isCooldown_Q = false;
            }
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

        if (isCooldown_D)
        {
            abilityImage_D.fillAmount -= 1 / cooldown_D * Time.deltaTime;

            if (abilityImage_D.fillAmount <= 0)
            {
                abilityImage_D.fillAmount = 0;
                isCooldown_D = false;
            }
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

    void Skill_Q()
    {
        if(skillPoint_Q < 1)
        {
            return;
        }

        // 스킬Q의 쿨타임 UI 이미지
        if (isCooldown_Q == false)
        {
            PV.RPC("FireEffectRPC", RpcTarget.All);

            isCooldown_Q = true;
            skillImage_Q.fillAmount = 1;
        }
    }

    [PunRPC]
    void FireEffectRPC()
    {
        StartCoroutine(FireAttack());
    }

    IEnumerator FireAttack()
    {
        // 스킬Q사용 시 3초간 파티클과 공격력 상승
        isActive = true;
        float originAttackDmg = stat.Status.atk;
        stat.Status.atk += stat.Status.atk * 0.5f;
        fire.Play();
        
        yield return new WaitForSeconds(3.0f);
        isActive = false;
        stat.Status.atk = originAttackDmg;
        fire.Stop();
    }

    /// <summary>
    /// Q 스킬 발동 시 3초간 파티클을 활성화 하며 데미지를 1.5배로 올리는 코루틴
    /// <summary>
    IEnumerator FireBuff()
    {
        float originAtk = stat.Status.atk;
        isActive = true;
        stat.Status.atk *= 1.5f;
        fire.Play();
        yield return new WaitForSeconds(3.0f);
        isActive = false;
        stat.Status.atk = originAtk;
        fire.Stop();
    }

    void Skill_W()
    {
        if (skillPoint_W < 1)
        {
            return;
        }

        // 스킬W의 쿨타임 UI 이미지
        if (isCooldown_W == false)
        {
            // 생명력++
            stat.Status.hp += 50 + (stat.Status.ap * 0.5f);

            isCooldown_W = true;
            skillImage_W.fillAmount = 1;
        }
    }

    void Skill_E()
    {
        if (skillPoint_E < 1)
        {
            return;
        }

        OffSkill();

        // 스킬E의 쿨타임 UI 이미지
        if (isCooldown_E == false)
        {
            skillshot.GetComponent<Image>().enabled = true;
            isSkillReady = true;

            // 다른 스킬샷 UI disable
            indicatorRangeCircle.GetComponent<Image>().enabled = false;
            targetCircle.GetComponent<Image>().enabled = false;
        }

        registeredSkill = null;
        registeredSkill += Callback_E;
    }

    void Callback_E()
    {
        if (skillshot.GetComponent<Image>().enabled)
        {
            isSkill_E = true;
            heroCombat.targetedEnemy = null;

            // Rotate
            Quaternion rotationToLookAt = Quaternion.LookRotation(position - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref moveScript.rotateVelocity, 0);

            transform.eulerAngles = new Vector3(0, rotationY, 0);

            moveScript.agent.SetDestination(transform.position);
            moveScript.agent.stoppingDistance = 0;

            if (canSkillshot_E)
            {
                isCooldown_E = true;
                skillImage_E.fillAmount = 1;

                // 애니메이션
                StartCoroutine(corSkill_E());
                isSkillReady = false;
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
        if (PV.IsMine)
        {
            var go = PhotonNetwork.Instantiate("Private/Prefabs/Weapons/Sword06", transform.position, transform.rotation);
        }
        // Instantiate(projPrefab_E, projSpawnPoint_E.transform.position, projSpawnPoint_E.transform.rotation);
    }

    void Skill_R()
    {
        if (skillPoint_R < 1)
        {
            return;
        }

        OffSkill();

        // 스킬R의 쿨타임 UI 이미지
        if (isCooldown_R == false)
        {
            indicatorRangeCircle.GetComponent<Image>().enabled = true;
            targetCircle.GetComponent<Image>().enabled = true;
            isSkillReady = true;

            // 다른 스킬샷 UI disable
            skillshot.GetComponent<Image>().enabled = false;
        }

        registeredSkill = Callback_R;
    }

    void Callback_R()
    {
        if (targetCircle.GetComponent<Image>().enabled == true)
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
                isSkillReady = false;
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
        if (PV.IsMine)
        {
            var go = PhotonNetwork.Instantiate("Private/Prefabs/Weapons/Sword07_R", projSpawnPoint_R.transform.position, projSpawnPoint_R.transform.rotation);
            // Instantiate(projPrefab_R, projSpawnPoint_R.transform.position, projSpawnPoint_R.transform.rotation);
        }
    }

    void Ability_D()
    {
        // 어빌리티D의 쿨타임 UI 이미지
        if (isCooldown_D == false)
        {
            isCooldown_D = true;
            abilityImage_D.fillAmount = 1;
        }
    }

    void Ability_F()
    {
        // 어빌리티F의 쿨타임 UI 이미지
        if (isCooldown_F == false)
        {
            isCooldown_F = true;
            abilityImage_F.fillAmount = 1;
        }
    }

    void SkillUP()
    {
        if (championManager.skillPoint >= 1)
        {
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
        championManager.skillPoint--;
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
        championManager.skillPoint--;
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
        championManager.skillPoint--;
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
        if (skillPoint_R == 1 || stat.Status.level < 6)
        {
            return;
        }

        skillPoint_R++;
        championManager.skillPoint--;
        R_SkillPoint1.color = Color.yellow;
    }

    /// <summary>
    /// 스킬을 취소할 경우 변경된 월드 스페이스 UI를 원래대로 되돌립니다.
    /// </summary>
    void OffSkill()
    {
        isSkillReady = false;
        skillshot.GetComponent<Image>().enabled = false;
        indicatorRangeCircle.GetComponent<Image>().enabled = false;
        targetCircle.GetComponent<Image>().enabled = false;
    }

    /// <summary>
    /// UI_ChampSkill을 가져와서 필요한 컴포넌트를 할당 받습니다.
    /// </summary>
    private void ConnectToUI()
    {
        UI_ChampSkill ui = Util.SearchChild<UI_ChampSkill>(Managers.UI.Root, null, true);
        GameObject ui_go = ui.gameObject;
        
        if (!ui)
        {
            Debug.LogError("Failed to find UI_ChampSkill");
            return;
        }

        Q_SkillPoint1 = Util.SearchChild<Image>(ui_go, "Q_Skill Point 1", true);
        Q_SkillPoint2 = Util.SearchChild<Image>(ui_go, "Q_Skill Point 2", true);
        Q_SkillPoint3 = Util.SearchChild<Image>(ui_go, "Q_Skill Point 3", true);

        W_SkillPoint1 = Util.SearchChild<Image>(ui_go, "W_Skill Point 1", true);
        W_SkillPoint2 = Util.SearchChild<Image>(ui_go, "W_Skill Point 2", true);
        W_SkillPoint3 = Util.SearchChild<Image>(ui_go, "W_Skill Point 3", true);

        E_SkillPoint1 = Util.SearchChild<Image>(ui_go, "E_Skill Point 1", true);
        E_SkillPoint2 = Util.SearchChild<Image>(ui_go, "E_Skill Point 2", true);
        E_SkillPoint3 = Util.SearchChild<Image>(ui_go, "E_Skill Point 3", true);

        R_SkillPoint1 = Util.SearchChild<Image>(ui_go, "R_Skill Point 1", true);

        skillImage_Q = Util.SearchChild<Image>(ui_go, "Skill_QImage_Cooldown", true);
        skillImage_W = Util.SearchChild<Image>(ui_go, "Skill_WImage_Cooldown", true);
        skillImage_E = Util.SearchChild<Image>(ui_go, "Skill_EImage_Cooldown", true);
        skillImage_R = Util.SearchChild<Image>(ui_go, "Skill_RImage_Cooldown", true);

        abilityImage_D = Util.SearchChild<Image>(ui_go, "Ability_DImage_Cooldown", true);
        abilityImage_F = Util.SearchChild<Image>(ui_go, "Ability_FImage_Cooldown", true);

        skillUpButton = Util.SearchChild(ui_go, "SkillPoint Up", true);

        Button increaseQ = Util.SearchChild<Button>(skillUpButton, "Q Up Button");
        increaseQ.onClick.AddListener(SkillPointUp_Q);

        Button increaseW = Util.SearchChild<Button>(skillUpButton, "W Up Button");
        increaseW.onClick.AddListener(SkillPointUp_W);

        Button increaseE = Util.SearchChild<Button>(skillUpButton, "E Up Button");
        increaseE.onClick.AddListener(SkillPointUp_E);

        Button increaseR = Util.SearchChild<Button>(skillUpButton, "R Up Button");
        increaseR.onClick.AddListener(SkillPointUp_R);
    }
}
