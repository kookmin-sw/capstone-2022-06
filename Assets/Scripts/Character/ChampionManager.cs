using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.AI;

public class ChampionManager : Controller
{
    public int skillPoint;
    public bool isDead;
    public bool isRevive;

    PlayerAnimation playerAnim;
    ChampionStat stat;
    Animator anim;
    NavMeshAgent agent;
    HeroCombat heroCombat;

    PhotonView PV;

    [SerializeField] GameObject currentAttacker;

    public int killCount = 0;
    public int deathCount = 0;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        playerAnim = GetComponent<PlayerAnimation>();
        stat = GetComponent<ChampionStat>();
        stat.Initialize("Mangoawl");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        heroCombat = GetComponent<HeroCombat>();
    }

    void Update()
    {
        if(stat.Status.level < 10)
        {
            LevelUp();
        }

        if (stat.Status.hp > stat.Status.maxHp)
        {
            stat.Status.hp = stat.Status.maxHp;
        }

        playerAnim.agent.speed = stat.Status.moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && PV.IsMine)
            PV.RPC("TestCount", RpcTarget.All);
    }

    void FixedUpdate()
    {
        if(!isDead)
        {
            stat.Status.healthRegen = 0.02f + stat.Status.level * 0.05f;
            stat.Status.hp += stat.Status.healthRegen;
        }
        
    }

    void LevelUp()
    {
        if(stat.Status.exp >= 100)
        {
            stat.Status.exp = 0;
            stat.Status.level++;
            skillPoint++;
        }
    }

    public override void TakeDamage(float damage, GameObject attacker = null)
    {
        if (isDead)
            return;

        if (attacker != null)
        {
            currentAttacker = attacker;
        }

        if (stat == null)
            Debug.Log("Error Stat");
        else
            stat.Status.hp -= stat.Status.atk * (100 / (100 + stat.Status.defense));

        if (stat.Status.hp <= 0)
        {
            if (PV.IsMine)
                PV.RPC("SetKDCount", RpcTarget.All);

            OnDie();
            return;
        }
    }

    public void OnDie()
    {
        isDead = true;
        anim.SetTrigger("doDie");

        if (Util.GetLocalPlayerId() <= 5)
        {
            // 블루팀 리스폰
            heroCombat.targetedEnemy = null;
            agent.Warp(new Vector3(-105, 0, -105));

            
        }
        else
        {
            // 레드팀 리스폰
            heroCombat.targetedEnemy = null;
            agent.Warp(new Vector3(105, 0, 105));
        }
    }

    [PunRPC]
    public void SetKDCount()
    {
        if (currentAttacker.tag == "Player")
        {
            currentAttacker.GetComponent<ChampionManager>().killCount++;
        }
        deathCount++;
    }

    [PunRPC]
    public void TestCount()
    {
        killCount++;
        deathCount++;
    }
}
