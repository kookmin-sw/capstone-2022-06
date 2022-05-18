using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ChampionManager : Controller
{
    public int skillPoint;
    public bool isDead = false;

    HeroCombat heroCombatScript;
    PlayerAnimation playerAnim;
    ChampionStat stat;
    Animator anim;

    [SerializeField] GameObject currentAttacker;

    void Start()
    {
        heroCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
        playerAnim = GetComponent<PlayerAnimation>();
        stat = GetComponent<ChampionStat>();
        stat.Initialize("Mangoawl");
        anim = GetComponent<Animator>();
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
            OnDie();
            return;
        }
    }

    public void OnDie()
    {
        if(Util.GetLocalPlayerId() <= 5)
        {
            // 블루팀 리스폰
            isDead = true;
            anim.SetTrigger("doDie");
            //stat.Status.hp = stat.Status.maxHp;
            //transform.Translate(new Vector3(-105, 0, -105));
        }
        else
        {
            // 레드팀 리스폰
            isDead = true;
            anim.SetTrigger("doDie");
            //stat.Status.hp = stat.Status.maxHp;
            //transform.Translate(new Vector3(105, 0, 105));
        }
    }
}
